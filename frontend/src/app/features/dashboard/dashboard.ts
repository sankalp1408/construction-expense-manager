import { CurrencyPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../../core/services/auth.service';
import { DashboardService } from '../../core/services/dashboard.service';
import { DashboardSummary } from '../../core/models/dashboard.model';
import { TranslatePipe } from '../../shared/pipes/translate.pipe';
import { PulseOnChangeDirective } from '../../shared/directives/pulse-on-change.directive';

@Component({
  selector: 'app-dashboard',
  imports: [CurrencyPipe, MatCardModule, MatIconModule, TranslatePipe, PulseOnChangeDirective],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private readonly dashboardService = inject(DashboardService);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  readonly currentUser = this.authService.currentUser;
  readonly summary = signal<DashboardSummary | null>(null);
  readonly loading = signal(false);

  constructor() {
    this.load();
  }

  load(): void {
    this.loading.set(true);

    this.dashboardService.getSummary({}).subscribe({
      next: (summary) => {
        this.summary.set(summary);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  goToTenderWorks(): void {
    this.router.navigate(['/tender-works']);
  }

  goToCommissionWorks(): void {
    this.router.navigate(['/commission-works']);
  }

  goToPrivateWorks(): void {
    this.router.navigate(['/private-works']);
  }
}
