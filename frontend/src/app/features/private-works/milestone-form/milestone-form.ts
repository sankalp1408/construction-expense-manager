import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { PrivateWorkMilestone } from '../../../core/models/private-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { fromIsoDate, toIsoDate } from '../../../shared/date-utils';

export interface MilestoneFormDialogData {
  milestone: PrivateWorkMilestone | null;
  nextSortOrder: number;
}

@Component({
  selector: 'app-milestone-form',
  imports: [
    ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatSelectModule,
    MatDatepickerModule, TranslatePipe
  ],
  templateUrl: './milestone-form.html',
  styleUrl: './milestone-form.scss'
})
export class MilestoneForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<MilestoneForm>);
  readonly data = inject<MilestoneFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.milestone !== null;
  private readonly m = this.data.milestone;

  readonly form = this.fb.nonNullable.group({
    stageName: [this.m?.stageName ?? '', Validators.required],
    percentOfTotal: [this.m?.percentOfTotal ?? 0],
    paidAmount: [this.m?.paidAmount ?? 0],
    paidDate: fromIsoDate(this.m?.paidDate),
    status: [this.m?.status ?? 'Pending', Validators.required],
    sortOrder: [this.m?.sortOrder ?? this.data.nextSortOrder]
  });

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.getRawValue();
    this.dialogRef.close({ ...value, paidDate: toIsoDate(value.paidDate) });
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
