import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CurrencyPipe } from '@angular/common';
import { GstVendorEntry, SaveGstVendorEntry } from '../../core/models/gst-vendor-entry.model';
import { TranslatePipe } from '../pipes/translate.pipe';
import { fromIsoDate, toIsoDate } from '../date-utils';

export interface GstVendorFormDialogData {
  entry: GstVendorEntry | null;
}

@Component({
  selector: 'app-gst-vendor-form',
  imports: [
    ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatDatepickerModule,
    CurrencyPipe, TranslatePipe
  ],
  templateUrl: './gst-vendor-form.html',
  styleUrl: './gst-vendor-form.scss'
})
export class GstVendorForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<GstVendorForm>);
  readonly data = inject<GstVendorFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.entry !== null;
  private readonly e = this.data.entry;

  readonly form = this.fb.nonNullable.group({
    vendorName: [this.e?.vendorName ?? '', Validators.required],
    gstBillAmount: [this.e?.gstBillAmount ?? 0],
    commissionPercent: [this.e?.commissionPercent ?? 0],
    sentDate: [fromIsoDate(this.e?.sentDate), Validators.required]
  });

  // Display-only preview of GstVendorLedgerService's commissionAmount formula
  // (GstBillAmount * CommissionPercent / 100) — not a form control, never submitted.
  get commissionAmountPreview(): number {
    const { gstBillAmount, commissionPercent } = this.form.getRawValue();
    return (gstBillAmount || 0) * (commissionPercent || 0) / 100;
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.getRawValue();
    const result: SaveGstVendorEntry = {
      vendorName: value.vendorName,
      gstBillAmount: value.gstBillAmount,
      commissionPercent: value.commissionPercent,
      sentDate: toIsoDate(value.sentDate)!
    };
    this.dialogRef.close(result);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
