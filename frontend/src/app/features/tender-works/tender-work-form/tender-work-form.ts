import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SaveTenderWork, TenderWork } from '../../../core/models/tender-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';

export interface TenderWorkFormDialogData {
  work: TenderWork | null;
}

@Component({
  selector: 'app-tender-work-form',
  imports: [ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, TranslatePipe],
  templateUrl: './tender-work-form.html',
  styleUrl: './tender-work-form.scss'
})
export class TenderWorkForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<TenderWorkForm>);
  readonly data = inject<TenderWorkFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.work !== null;
  private readonly w = this.data.work;

  readonly form = this.fb.nonNullable.group({
    tenderName: [this.w?.tenderName ?? '', Validators.required],
    nameOfWork: [this.w?.nameOfWork ?? '', Validators.required],
    tenderAmount: [this.w?.tenderAmount ?? 0],
    tenderFee: [this.w?.tenderFee ?? 0],
    tenderEMD: [this.w?.tenderEMD ?? 0],
    tenderFilingAmount: [this.w?.tenderFilingAmount ?? 0],
    gstTotal: [this.w?.gstTotal ?? 0],
    billedGst: [this.w?.billedGst ?? 0],
    extraGstBill: [this.w?.extraGstBill ?? 0],
    workExpenditure: [this.w?.workExpenditure ?? 0],
    securityDepositPercent: [this.w?.securityDepositPercent ?? 10],
    officeProtocolPercent: [this.w?.officeProtocolPercent ?? 6],
    corporatorName: [this.w?.corporatorName ?? ''],
    corporatorProtocolPercent: [this.w?.corporatorProtocolPercent ?? 10],
    gstBillCommission: [this.w?.gstBillCommission ?? 0]
  });

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.dialogRef.close(this.form.getRawValue() as SaveTenderWork);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
