import { Component, inject } from '@angular/core';
import { FormArray, FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { CurrencyPipe } from '@angular/common';
import {
  PrivateWorkDepartmentalLabour, SavePrivateWorkDepartmentalLabour, SavePrivateWorkDepartmentalLabourRow
} from '../../../core/models/private-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { fromIsoDate, toIsoDate } from '../../../shared/date-utils';

export interface DepartmentalLabourFormDialogData {
  entry: PrivateWorkDepartmentalLabour | null;
}

@Component({
  selector: 'app-departmental-labour-form',
  imports: [
    ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatIconModule,
    MatDatepickerModule, CurrencyPipe, TranslatePipe
  ],
  templateUrl: './departmental-labour-form.html',
  styleUrl: './departmental-labour-form.scss'
})
export class DepartmentalLabourForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<DepartmentalLabourForm>);
  readonly data = inject<DepartmentalLabourFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.entry !== null;
  private readonly e = this.data.entry;

  readonly form = this.fb.nonNullable.group({
    labourDate: [fromIsoDate(this.e?.labourDate), Validators.required],
    rows: this.fb.array(
      (this.e?.rows.length ? this.e.rows : [{ labourType: '', count: 0, rate: 0 }]).map((r) => this.buildRow(r))
    )
  });

  get rows(): FormArray {
    return this.form.controls.rows;
  }

  get grandTotal(): number {
    return this.rows.controls.reduce((sum: number, _, i) => sum + this.rowSubtotal(i), 0);
  }

  private buildRow(row: SavePrivateWorkDepartmentalLabourRow) {
    return this.fb.nonNullable.group({
      labourType: [row.labourType, Validators.required],
      count: [row.count],
      rate: [row.rate]
    });
  }

  rowSubtotal(index: number): number {
    const row = this.rows.at(index).getRawValue() as SavePrivateWorkDepartmentalLabourRow;
    return (row.count || 0) * (row.rate || 0);
  }

  addRow(): void {
    this.rows.push(this.buildRow({ labourType: '', count: 0, rate: 0 }));
  }

  removeRow(index: number): void {
    if (this.rows.length > 1) {
      this.rows.removeAt(index);
    }
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.getRawValue();
    const result: SavePrivateWorkDepartmentalLabour = {
      labourDate: toIsoDate(value.labourDate)!,
      rows: value.rows
    };
    this.dialogRef.close(result);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
