import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { GstVendorRepayment, SaveGstVendorRepayment } from '../../core/models/gst-vendor-entry.model';
import { TranslatePipe } from '../pipes/translate.pipe';
import { fromIsoDate, toIsoDate } from '../date-utils';

export interface GstVendorRepaymentFormDialogData {
  repayment: GstVendorRepayment | null;
}

@Component({
  selector: 'app-gst-vendor-repayment-form',
  imports: [
    ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatSelectModule,
    MatDatepickerModule, TranslatePipe
  ],
  templateUrl: './gst-vendor-repayment-form.html',
  styleUrl: './gst-vendor-repayment-form.scss'
})
export class GstVendorRepaymentForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<GstVendorRepaymentForm>);
  readonly data = inject<GstVendorRepaymentFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.repayment !== null;
  private readonly r = this.data.repayment;

  readonly form = this.fb.nonNullable.group({
    receivedDate: [fromIsoDate(this.r?.receivedDate), Validators.required],
    amountReceived: [this.r?.amountReceived ?? 0],
    mode: [this.r?.mode ?? 'Cash', Validators.required]
  });

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.getRawValue();
    const result: SaveGstVendorRepayment = {
      receivedDate: toIsoDate(value.receivedDate)!,
      amountReceived: value.amountReceived,
      mode: value.mode
    };
    this.dialogRef.close(result);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
