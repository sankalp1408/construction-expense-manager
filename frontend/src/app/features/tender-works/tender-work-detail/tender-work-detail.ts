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
import { TenderWorkService } from '../../../core/services/tender-work.service';
import { GstVendorService } from '../../../core/services/gst-vendor.service';
import { TenderRaBill, TenderWork } from '../../../core/models/tender-work.model';
import { GstVendorEntry, GstVendorLedger, GstVendorRepayment } from '../../../core/models/gst-vendor-entry.model';
import { TenderWorkForm, TenderWorkFormDialogData } from '../tender-work-form/tender-work-form';
import { RaBillForm, RaBillFormDialogData } from '../ra-bill-form/ra-bill-form';
import { GstVendorForm, GstVendorFormDialogData } from '../../../shared/gst-vendor-form/gst-vendor-form';
import {
  GstVendorRepaymentForm, GstVendorRepaymentFormDialogData
} from '../../../shared/gst-vendor-repayment-form/gst-vendor-repayment-form';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { LanguageService } from '../../../core/services/language.service';
import { PulseOnChangeDirective } from '../../../shared/directives/pulse-on-change.directive';

@Component({
  selector: 'app-tender-work-detail',
  imports: [
    CurrencyPipe, DatePipe, MatCardModule, MatButtonModule, MatIconModule, MatTableModule, MatChipsModule,
    MatExpansionModule, TranslatePipe, PulseOnChangeDirective
  ],
  templateUrl: './tender-work-detail.html',
  styleUrl: './tender-work-detail.scss'
})
export class TenderWorkDetail {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly service = inject(TenderWorkService);
  private readonly gstVendorService = inject(GstVendorService);
  private readonly dialog = inject(MatDialog);
  private readonly authService = inject(AuthService);
  private readonly languageService = inject(LanguageService);

  readonly isSuperAdmin = this.authService.isSuperAdmin;
  readonly work = signal<TenderWork | null>(null);
  readonly raBills = signal<TenderRaBill[]>([]);
  readonly gstLedger = signal<GstVendorLedger | null>(null);

  readonly raBillColumns = ['raBillNumber', 'billDate', 'billedAmount', 'corporatorCommissionAmount', 'officerCommissionAmount', 'actions'];
  readonly repaymentColumns = ['receivedDate', 'amountReceived', 'mode', 'actions'];

  private readonly workId = Number(this.route.snapshot.paramMap.get('id'));

  constructor() {
    this.loadAll();
  }

  loadAll(): void {
    this.service.getById(this.workId).subscribe((w) => this.work.set(w));
    this.service.getRaBills(this.workId).subscribe((bills) => this.raBills.set(bills));
    this.gstVendorService.getLedger('tender-works', this.workId).subscribe((ledger) => this.gstLedger.set(ledger));
  }

  goBack(): void {
    this.router.navigate(['/tender-works']);
  }

  editWork(): void {
    const current = this.work();
    if (!current) return;

    const ref = this.dialog.open<TenderWorkForm, TenderWorkFormDialogData>(TenderWorkForm, { data: { work: current } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.update(this.workId, result).subscribe((w) => this.work.set(w));
    });
  }

  deleteWork(): void {
    if (!confirm(this.languageService.translate('tender.confirmDeleteTender'))) return;
    this.service.delete(this.workId).subscribe(() => this.router.navigate(['/tender-works']));
  }

  addRaBill(): void {
    const ref = this.dialog.open<RaBillForm, RaBillFormDialogData>(RaBillForm, { data: { bill: null } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.addRaBill(this.workId, result).subscribe(() => this.loadAll());
    });
  }

  editRaBill(bill: TenderRaBill): void {
    const ref = this.dialog.open<RaBillForm, RaBillFormDialogData>(RaBillForm, { data: { bill } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.updateRaBill(this.workId, bill.id, result).subscribe(() => this.loadAll());
    });
  }

  deleteRaBill(bill: TenderRaBill): void {
    if (!confirm(this.languageService.translate('tender.confirmDeleteRaBill'))) return;
    this.service.deleteRaBill(this.workId, bill.id).subscribe(() => this.loadAll());
  }

  addGstVendor(): void {
    const ref = this.dialog.open<GstVendorForm, GstVendorFormDialogData>(GstVendorForm, { data: { entry: null } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.gstVendorService.add('tender-works', this.workId, result).subscribe(() => this.loadAll());
    });
  }

  editGstVendor(entry: GstVendorEntry): void {
    const ref = this.dialog.open<GstVendorForm, GstVendorFormDialogData>(GstVendorForm, { data: { entry } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.gstVendorService.update('tender-works', this.workId, entry.id, result).subscribe(() => this.loadAll());
    });
  }

  deleteGstVendor(entry: GstVendorEntry): void {
    if (!confirm(this.languageService.translate('tender.confirmDeleteVendorEntry'))) return;
    this.gstVendorService.delete('tender-works', this.workId, entry.id).subscribe(() => this.loadAll());
  }

  addRepayment(entry: GstVendorEntry): void {
    const ref = this.dialog.open<GstVendorRepaymentForm, GstVendorRepaymentFormDialogData>(GstVendorRepaymentForm, {
      data: { repayment: null }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.gstVendorService.addRepayment('tender-works', this.workId, entry.id, result).subscribe(() => this.loadAll());
    });
  }

  editRepayment(entry: GstVendorEntry, repayment: GstVendorRepayment): void {
    const ref = this.dialog.open<GstVendorRepaymentForm, GstVendorRepaymentFormDialogData>(GstVendorRepaymentForm, {
      data: { repayment }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.gstVendorService.updateRepayment('tender-works', this.workId, entry.id, repayment.id, result).subscribe(() => this.loadAll());
    });
  }

  deleteRepayment(entry: GstVendorEntry, repayment: GstVendorRepayment): void {
    if (!confirm(this.languageService.translate('gstVendor.confirmDeleteRepayment'))) return;
    this.gstVendorService.deleteRepayment('tender-works', this.workId, entry.id, repayment.id).subscribe(() => this.loadAll());
  }
}
