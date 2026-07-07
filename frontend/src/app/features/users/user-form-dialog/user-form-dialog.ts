import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { User } from '../../../core/models/user.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';

export interface UserFormDialogData {
  user: User | null; // null => create mode
}

@Component({
  selector: 'app-user-form-dialog',
  imports: [ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatSlideToggleModule, TranslatePipe],
  templateUrl: './user-form-dialog.html',
  styleUrl: './user-form-dialog.scss'
})
export class UserFormDialog {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<UserFormDialog>);
  readonly data = inject<UserFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.user !== null;

  readonly form = this.fb.nonNullable.group({
    name: [this.data.user?.name ?? '', Validators.required],
    mobileNumber: [this.data.user?.mobileNumber ?? '', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
    isActive: [this.data.user?.isActive ?? true]
  });

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
