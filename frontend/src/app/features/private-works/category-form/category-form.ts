import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { DEFAULT_CATEGORY_PRESET_KEYS, PrivateWorkCategory } from '../../../core/models/private-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { LanguageService } from '../../../core/services/language.service';

export interface CategoryFormDialogData {
  category: PrivateWorkCategory | null;
}

@Component({
  selector: 'app-category-form',
  imports: [ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatAutocompleteModule, TranslatePipe],
  templateUrl: './category-form.html',
  styleUrl: './category-form.scss'
})
export class CategoryForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<CategoryForm>);
  private readonly languageService = inject(LanguageService);
  readonly data = inject<CategoryFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.category !== null;
  readonly categoryPresets = DEFAULT_CATEGORY_PRESET_KEYS.map((key) => this.languageService.translate(key));
  private readonly c = this.data.category;

  readonly form = this.fb.nonNullable.group({
    categoryName: [this.c?.categoryName ?? '', Validators.required],
    workerName: [this.c?.workerName ?? ''],
    rateBasis: [this.c?.rateBasis ?? ''],
    agreedTotalAmount: [this.c?.agreedTotalAmount ?? 0]
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
