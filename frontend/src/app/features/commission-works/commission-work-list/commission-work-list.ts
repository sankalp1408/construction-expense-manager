import { CurrencyPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { CommissionWorkService } from '../../../core/services/commission-work.service';
import { AuthService } from '../../../core/services/auth.service';
import { CommissionWork } from '../../../core/models/commission-work.model';
import { CommissionWorkForm, CommissionWorkFormDialogData } from '../commission-work-form/commission-work-form';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';

@Component({
  selector: 'app-commission-work-list',
  imports: [CurrencyPipe, MatButtonModule, MatCardModule, MatIconModule, TranslatePipe],
  templateUrl: './commission-work-list.html',
  styleUrl: './commission-work-list.scss'
})
export class CommissionWorkList {
  private readonly service = inject(CommissionWorkService);
  private readonly dialog = inject(MatDialog);
  private readonly router = inject(Router);
  private readonly authService = inject(AuthService);

  readonly isSuperAdmin = this.authService.isSuperAdmin;
  readonly works = signal<CommissionWork[]>([]);

  constructor() {
    this.load();
  }

  load(): void {
    this.service.getAll().subscribe((works) => this.works.set(works));
  }

  openCreateDialog(): void {
    const ref = this.dialog.open<CommissionWorkForm, CommissionWorkFormDialogData>(CommissionWorkForm, {
      data: { work: null }
    });

    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.create(result).subscribe(() => this.load());
    });
  }

  openDetail(work: CommissionWork): void {
    this.router.navigate(['/commission-works', work.id]);
  }
}
