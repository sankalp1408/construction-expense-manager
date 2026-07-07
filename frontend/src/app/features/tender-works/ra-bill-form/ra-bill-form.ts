import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SaveTenderRaBill, TenderRaBill } from '../../../core/models/tender-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { fromIsoDate, toIsoDate } from '../../../shared/date-utils';

export interface RaBillFormDialogData {
  bill: TenderRaBill | null;
}

@Component({
  selector: 'app-ra-bill-form',
  imports: [ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, TranslatePipe],
  templateUrl: './ra-bill-form.html',
  styleUrl: './ra-bill-form.scss'
})
export class RaBillForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<RaBillForm>);
  readonly data = inject<RaBillFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.bill !== null;
  private readonly b = this.data.bill;

  readonly form = this.fb.nonNullable.group({
    raBillNumber: [this.b?.raBillNumber ?? '', Validators.required],
    billDate: [fromIsoDate(this.b?.billDate), Validators.required],
    billedAmount: [this.b?.billedAmount ?? 0],
    corporatorCommissionPercent: [this.b?.corporatorCommissionPercent ?? 10],
    officerCommissionPercent: [this.b?.officerCommissionPercent ?? 8],
    remarks: [this.b?.remarks ?? '']
  });

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.getRawValue();
    const result: SaveTenderRaBill = { ...value, billDate: toIsoDate(value.billDate)! };
    this.dialogRef.close(result);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
