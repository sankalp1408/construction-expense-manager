import { CurrencyPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { TenderWorkService } from '../../../core/services/tender-work.service';
import { AuthService } from '../../../core/services/auth.service';
import { TenderWork } from '../../../core/models/tender-work.model';
import { TenderWorkForm, TenderWorkFormDialogData } from '../tender-work-form/tender-work-form';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';

@Component({
  selector: 'app-tender-work-list',
  imports: [CurrencyPipe, MatButtonModule, MatCardModule, MatIconModule, TranslatePipe],
  templateUrl: './tender-work-list.html',
  styleUrl: './tender-work-list.scss'
})
export class TenderWorkList {
  private readonly service = inject(TenderWorkService);
  private readonly dialog = inject(MatDialog);
  private readonly router = inject(Router);
  private readonly authService = inject(AuthService);

  readonly isSuperAdmin = this.authService.isSuperAdmin;
  readonly works = signal<TenderWork[]>([]);

  constructor() {
    this.load();
  }

  load(): void {
    this.service.getAll().subscribe((works) => this.works.set(works));
  }

  openCreateDialog(): void {
    const ref = this.dialog.open<TenderWorkForm, TenderWorkFormDialogData>(TenderWorkForm, {
      data: { work: null }
    });

    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.create(result).subscribe(() => this.load());
    });
  }

  openDetail(work: TenderWork): void {
    this.router.navigate(['/tender-works', work.id]);
  }
}
