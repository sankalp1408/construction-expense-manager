import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { PrivateWorkCategoryPayment, SavePrivateWorkCategoryPayment } from '../../../core/models/private-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { fromIsoDate, toIsoDate } from '../../../shared/date-utils';

export interface CategoryPaymentFormDialogData {
  payment: PrivateWorkCategoryPayment | null;
}

@Component({
  selector: 'app-category-payment-form',
  imports: [ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, TranslatePipe],
  templateUrl: './category-payment-form.html',
  styleUrl: './category-payment-form.scss'
})
export class CategoryPaymentForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<CategoryPaymentForm>);
  readonly data = inject<CategoryPaymentFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.payment !== null;
  private readonly p = this.data.payment;

  readonly form = this.fb.nonNullable.group({
    paymentDate: [fromIsoDate(this.p?.paymentDate), Validators.required],
    amount: [this.p?.amount ?? 0],
    remarks: [this.p?.remarks ?? '']
  });

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.getRawValue();
    const result: SavePrivateWorkCategoryPayment = { ...value, paymentDate: toIsoDate(value.paymentDate)! };
    this.dialogRef.close(result);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
