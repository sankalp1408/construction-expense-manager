import { CurrencyPipe, DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { ReportService } from '../../core/services/report.service';
import { ReportPeriod, TenderReport } from '../../core/models/report.model';
import { TranslatePipe } from '../../shared/pipes/translate.pipe';
import { LanguageService } from '../../core/services/language.service';

const PERIODS: { value: ReportPeriod; label: string }[] = [
  { value: 'Q1', label: 'Q1' },
  { value: 'Q2', label: 'Q2' },
  { value: 'Q3', label: 'Q3' },
  { value: 'Q4', label: 'Q4' },
  { value: 'H1', label: 'H1' },
  { value: 'H2', label: 'H2' },
  { value: 'Yearly', label: 'Yearly' }
];

@Component({
  selector: 'app-reports',
  imports: [
    FormsModule, CurrencyPipe, DatePipe, MatCardModule, MatFormFieldModule, MatSelectModule, MatButtonToggleModule, MatTableModule,
    TranslatePipe
  ],
  templateUrl: './reports.html',
  styleUrl: './reports.scss'
})
export class Reports {
  private readonly reportService = inject(ReportService);
  private readonly languageService = inject(LanguageService);

  readonly periods = PERIODS;
  readonly fyOptions = this.buildFyOptions();
  readonly report = signal<TenderReport | null>(null);
  readonly vendorColumns = ['vendorName', 'totalGstBillAmount', 'totalCommission', 'totalPaidToVendor', 'totalCashReturned'];

  fyStartYear = this.currentFyStartYear();
  period: ReportPeriod = 'Yearly';

  constructor() {
    this.load();
  }

  load(): void {
    this.reportService.getTenderReport({ fyStartYear: this.fyStartYear, period: this.period })
      .subscribe((report) => this.report.set(report));
  }

  periodDisplayLabel(): string {
    const periodText = this.period === 'Yearly'
      ? this.languageService.translate('reports.periodYearly')
      : this.period;

    return this.languageService.translate('reports.periodLabel', {
      period: periodText,
      fyStart: this.fyStartYear,
      fyEnd: (this.fyStartYear + 1).toString().slice(-2)
    });
  }

  private currentFyStartYear(): number {
    const now = new Date();
    const year = now.getFullYear();
    return now.getMonth() + 1 >= 4 ? year : year - 1;
  }

  private buildFyOptions(): { value: number; label: string }[] {
    const current = this.currentFyStartYear();
    const options = [];
    for (let y = current; y > current - 5; y--) {
      options.push({ value: y, label: `FY ${y}-${(y + 1).toString().slice(-2)}` });
    }
    return options;
  }
}
