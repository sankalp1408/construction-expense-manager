import { CurrencyPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { DashboardService } from '../../core/services/dashboard.service';
import { DashboardQuery, DashboardSummary } from '../../core/models/dashboard.model';
import { TranslatePipe } from '../../shared/pipes/translate.pipe';

@Component({
  selector: 'app-dashboard',
  imports: [
    FormsModule, CurrencyPipe, MatCardModule, MatButtonModule, MatFormFieldModule, MatInputModule,
    MatDatepickerModule, TranslatePipe
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private readonly dashboardService = inject(DashboardService);

  readonly summary = signal<DashboardSummary | null>(null);
  readonly loading = signal(false);

  fromDate: Date | null = null;
  toDate: Date | null = null;

  constructor() {
    this.load();
  }

  load(): void {
    this.loading.set(true);
    const query: DashboardQuery = {
      fromDate: this.toIsoDate(this.fromDate),
      toDate: this.toIsoDate(this.toDate)
    };

    this.dashboardService.getSummary(query).subscribe({
      next: (summary) => {
        this.summary.set(summary);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  clearFilters(): void {
    this.fromDate = null;
    this.toDate = null;
    this.load();
  }

  private toIsoDate(date: Date | null): string | undefined {
    if (!date) return undefined;
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
}
