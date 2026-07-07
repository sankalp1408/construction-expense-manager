import { Component, inject, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { UserService } from '../../core/services/user.service';
import { User } from '../../core/models/user.model';
import { UserFormDialog, UserFormDialogData } from './user-form-dialog/user-form-dialog';
import { TranslatePipe } from '../../shared/pipes/translate.pipe';
import { LanguageService } from '../../core/services/language.service';

@Component({
  selector: 'app-user-list',
  imports: [MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatDialogModule, TranslatePipe],
  templateUrl: './user-list.html',
  styleUrl: './user-list.scss'
})
export class UserList {
  private readonly userService = inject(UserService);
  private readonly dialog = inject(MatDialog);
  private readonly languageService = inject(LanguageService);

  readonly users = signal<User[]>([]);
  readonly displayedColumns = ['name', 'mobileNumber', 'role', 'status', 'actions'];

  constructor() {
    this.load();
  }

  load(): void {
    this.userService.getAll().subscribe((users) => this.users.set(users));
  }

  openCreateDialog(): void {
    const ref = this.dialog.open<UserFormDialog, UserFormDialogData>(UserFormDialog, {
      data: { user: null }
    });

    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.userService.create({ name: result.name, mobileNumber: result.mobileNumber, role: 'Manager' })
        .subscribe(() => this.load());
    });
  }

  openEditDialog(user: User): void {
    const ref = this.dialog.open<UserFormDialog, UserFormDialogData>(UserFormDialog, {
      data: { user }
    });

    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.userService.update(user.id, result).subscribe(() => this.load());
    });
  }

  deactivate(user: User): void {
    if (!confirm(this.languageService.translate('users.confirmDeactivate', { name: user.name }))) return;
    this.userService.deactivate(user.id).subscribe(() => this.load());
  }
}
