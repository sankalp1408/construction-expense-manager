import { CurrencyPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { PrivateWorkService } from '../../../core/services/private-work.service';
import { PrivateWork } from '../../../core/models/private-work.model';
import { PrivateWorkForm, PrivateWorkFormDialogData } from '../private-work-form/private-work-form';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';

@Component({
  selector: 'app-private-work-list',
  imports: [CurrencyPipe, MatButtonModule, MatCardModule, MatIconModule, TranslatePipe],
  templateUrl: './private-work-list.html',
  styleUrl: './private-work-list.scss'
})
export class PrivateWorkList {
  private readonly service = inject(PrivateWorkService);
  private readonly dialog = inject(MatDialog);
  private readonly router = inject(Router);

  readonly works = signal<PrivateWork[]>([]);

  constructor() {
    this.load();
  }

  load(): void {
    this.service.getAll().subscribe((works) => this.works.set(works));
  }

  openCreateDialog(): void {
    const ref = this.dialog.open<PrivateWorkForm, PrivateWorkFormDialogData>(PrivateWorkForm, {
      data: { work: null }
    });

    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.create(result).subscribe(() => this.load());
    });
  }

  openDetail(work: PrivateWork): void {
    this.router.navigate(['/private-works', work.id]);
  }
}
