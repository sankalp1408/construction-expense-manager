import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { PrivateWorkMaterial, SavePrivateWorkMaterial } from '../../../core/models/private-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { fromIsoDate, toIsoDate } from '../../../shared/date-utils';

export interface MaterialFormDialogData {
  material: PrivateWorkMaterial | null;
}

@Component({
  selector: 'app-material-form',
  imports: [ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, TranslatePipe],
  templateUrl: './material-form.html',
  styleUrl: './material-form.scss'
})
export class MaterialForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<MaterialForm>);
  readonly data = inject<MaterialFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.material !== null;
  private readonly m = this.data.material;

  readonly form = this.fb.nonNullable.group({
    materialName: [this.m?.materialName ?? '', Validators.required],
    vendorName: [this.m?.vendorName ?? ''],
    amount: [this.m?.amount ?? 0],
    paymentDate: [fromIsoDate(this.m?.paymentDate), Validators.required]
  });

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.getRawValue();
    const result: SavePrivateWorkMaterial = { ...value, paymentDate: toIsoDate(value.paymentDate)! };
    this.dialogRef.close(result);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
