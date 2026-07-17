import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CurrencyPipe } from '@angular/common';
import { PrivateWork } from '../../../core/models/private-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';

export interface PrivateWorkFormDialogData {
  work: PrivateWork | null;
}

@Component({
  selector: 'app-private-work-form',
  imports: [ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, CurrencyPipe, TranslatePipe],
  templateUrl: './private-work-form.html',
  styleUrl: './private-work-form.scss'
})
export class PrivateWorkForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<PrivateWorkForm>);
  readonly data = inject<PrivateWorkFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.work !== null;
  private readonly w = this.data.work;

  readonly form = this.fb.nonNullable.group({
    clientName: [this.w?.clientName ?? '', Validators.required],
    workDescription: [this.w?.workDescription ?? ''],
    areaSqft: [this.w?.areaSqft ?? 0],
    ratePerSqft: [this.w?.ratePerSqft ?? 0]
  });

  // Display-only preview of PrivateWorkService.ToDto's TotalAmount formula
  // (AreaSqft * RatePerSqft) — not a form control, never submitted.
  get totalAmountPreview(): number {
    const { areaSqft, ratePerSqft } = this.form.getRawValue();
    return (areaSqft || 0) * (ratePerSqft || 0);
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
