import { CurrencyPipe, DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog } from '@angular/material/dialog';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { AuthService } from '../../../core/services/auth.service';
import { CommissionWorkService } from '../../../core/services/commission-work.service';
import { GstVendorService } from '../../../core/services/gst-vendor.service';
import { CommissionWork } from '../../../core/models/commission-work.model';
import { GstVendorEntry, GstVendorLedger, GstVendorRepayment } from '../../../core/models/gst-vendor-entry.model';
import { CommissionWorkForm, CommissionWorkFormDialogData } from '../commission-work-form/commission-work-form';
import { GstVendorForm, GstVendorFormDialogData } from '../../../shared/gst-vendor-form/gst-vendor-form';
import {
  GstVendorRepaymentForm, GstVendorRepaymentFormDialogData
} from '../../../shared/gst-vendor-repayment-form/gst-vendor-repayment-form';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { LanguageService } from '../../../core/services/language.service';

@Component({
  selector: 'app-commission-work-detail',
  imports: [
    CurrencyPipe, DatePipe, MatCardModule, MatButtonModule, MatIconModule, MatTableModule, MatChipsModule,
    MatExpansionModule, TranslatePipe
  ],
  templateUrl: './commission-work-detail.html',
  styleUrl: './commission-work-detail.scss'
})
export class CommissionWorkDetail {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly service = inject(CommissionWorkService);
  private readonly gstVendorService = inject(GstVendorService);
  private readonly dialog = inject(MatDialog);
  private readonly authService = inject(AuthService);
  private readonly languageService = inject(LanguageService);

  readonly isSuperAdmin = this.authService.isSuperAdmin;
  readonly work = signal<CommissionWork | null>(null);
  readonly gstLedger = signal<GstVendorLedger | null>(null);
  readonly repaymentColumns = ['receivedDate', 'amountReceived', 'mode', 'actions'];

  private readonly workId = Number(this.route.snapshot.paramMap.get('id'));

  constructor() {
    this.loadAll();
  }

  loadAll(): void {
    this.service.getById(this.workId).subscribe((w) => this.work.set(w));
    this.gstVendorService.getLedger('commission-works', this.workId).subscribe((ledger) => this.gstLedger.set(ledger));
  }

  goBack(): void {
    this.router.navigate(['/commission-works']);
  }

  editWork(): void {
    const current = this.work();
    if (!current) return;

    const ref = this.dialog.open<CommissionWorkForm, CommissionWorkFormDialogData>(CommissionWorkForm, { data: { work: current } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.update(this.workId, result).subscribe((w) => this.work.set(w));
    });
  }

  deleteWork(): void {
    if (!confirm(this.languageService.translate('commission.confirmDelete'))) return;
    this.service.delete(this.workId).subscribe(() => this.router.navigate(['/commission-works']));
  }

  addGstVendor(): void {
    const ref = this.dialog.open<GstVendorForm, GstVendorFormDialogData>(GstVendorForm, { data: { entry: null } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.gstVendorService.add('commission-works', this.workId, result).subscribe(() => this.loadAll());
    });
  }

  editGstVendor(entry: GstVendorEntry): void {
    const ref = this.dialog.open<GstVendorForm, GstVendorFormDialogData>(GstVendorForm, { data: { entry } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.gstVendorService.update('commission-works', this.workId, entry.id, result).subscribe(() => this.loadAll());
    });
  }

  deleteGstVendor(entry: GstVendorEntry): void {
    if (!confirm(this.languageService.translate('tender.confirmDeleteVendorEntry'))) return;
    this.gstVendorService.delete('commission-works', this.workId, entry.id).subscribe(() => this.loadAll());
  }

  addRepayment(entry: GstVendorEntry): void {
    const ref = this.dialog.open<GstVendorRepaymentForm, GstVendorRepaymentFormDialogData>(GstVendorRepaymentForm, {
      data: { repayment: null }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.gstVendorService.addRepayment('commission-works', this.workId, entry.id, result).subscribe(() => this.loadAll());
    });
  }

  editRepayment(entry: GstVendorEntry, repayment: GstVendorRepayment): void {
    const ref = this.dialog.open<GstVendorRepaymentForm, GstVendorRepaymentFormDialogData>(GstVendorRepaymentForm, {
      data: { repayment }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.gstVendorService.updateRepayment('commission-works', this.workId, entry.id, repayment.id, result).subscribe(() => this.loadAll());
    });
  }

  deleteRepayment(entry: GstVendorEntry, repayment: GstVendorRepayment): void {
    if (!confirm(this.languageService.translate('gstVendor.confirmDeleteRepayment'))) return;
    this.gstVendorService.deleteRepayment('commission-works', this.workId, entry.id, repayment.id).subscribe(() => this.loadAll());
  }
}
