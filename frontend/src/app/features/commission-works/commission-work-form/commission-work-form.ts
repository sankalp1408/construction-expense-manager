import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CurrencyPipe } from '@angular/common';
import { CommissionWork } from '../../../core/models/commission-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';

export interface CommissionWorkFormDialogData {
  work: CommissionWork | null;
}

@Component({
  selector: 'app-commission-work-form',
  imports: [
    ReactiveFormsModule, MatDialogModule, MatButtonModule, MatExpansionModule, MatFormFieldModule, MatInputModule,
    CurrencyPipe, TranslatePipe
  ],
  templateUrl: './commission-work-form.html',
  styleUrl: './commission-work-form.scss'
})
export class CommissionWorkForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<CommissionWorkForm>);
  readonly data = inject<CommissionWorkFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.work !== null;
  private readonly w = this.data.work;

  readonly form = this.fb.nonNullable.group({
    workName: [this.w?.workName ?? '', Validators.required],
    partyName: [this.w?.partyName ?? '', Validators.required],
    tenderWorkAmount: [this.w?.tenderWorkAmount ?? 0],
    commissionPercent: [this.w?.commissionPercent ?? 0],
    gstAmount: [this.w?.gstAmount ?? 0],
    billedGst: [this.w?.billedGst ?? 0],
    extraGstBill: [this.w?.extraGstBill ?? 0],
    gstBillCommission: [this.w?.gstBillCommission ?? 0]
  });

  // Display-only preview of CommissionWorkService.ToDto's CommissionAmount
  // formula (TenderWorkAmount * CommissionPercent / 100) — not a form
  // control, never submitted.
  get commissionAmountPreview(): number {
    const { tenderWorkAmount, commissionPercent } = this.form.getRawValue();
    return (tenderWorkAmount || 0) * (commissionPercent || 0) / 100;
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.dialogRef.close(this.form.getRawValue());
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
