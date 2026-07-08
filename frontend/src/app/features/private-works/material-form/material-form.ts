import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CurrencyPipe } from '@angular/common';
import { MATERIAL_TYPE_PRESETS, PrivateWorkMaterial, SavePrivateWorkMaterial } from '../../../core/models/private-work.model';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { LanguageService } from '../../../core/services/language.service';
import { fromIsoDate, toIsoDate } from '../../../shared/date-utils';

export interface MaterialFormDialogData {
  material: PrivateWorkMaterial | null;
}

const CUSTOM_MATERIAL_TYPE = 'custom';

@Component({
  selector: 'app-material-form',
  imports: [
    ReactiveFormsModule, MatDialogModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatSelectModule,
    MatDatepickerModule, CurrencyPipe, TranslatePipe
  ],
  templateUrl: './material-form.html',
  styleUrl: './material-form.scss'
})
export class MaterialForm {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<MaterialForm>);
  private readonly languageService = inject(LanguageService);
  readonly data = inject<MaterialFormDialogData>(MAT_DIALOG_DATA);

  readonly isEditMode = this.data.material !== null;
  private readonly m = this.data.material;
  readonly customOption = CUSTOM_MATERIAL_TYPE;

  readonly materialTypePresets = MATERIAL_TYPE_PRESETS.map((p) => ({
    name: this.languageService.translate(p.nameKey),
    unit: this.languageService.translate(p.unitKey)
  }));

  readonly form = this.fb.nonNullable.group({
    materialType: [this.initialMaterialType(), Validators.required],
    materialName: [this.m?.materialName ?? '', Validators.required],
    unit: [this.m?.unit ?? '', Validators.required],
    vendorName: [this.m?.vendorName ?? ''],
    quantity: [this.m?.quantity ?? 0],
    rate: [this.m?.rate ?? 0],
    paymentDate: [fromIsoDate(this.m?.paymentDate), Validators.required]
  });

  get computedAmount(): number {
    const { quantity, rate } = this.form.getRawValue();
    return (quantity || 0) * (rate || 0);
  }

  private initialMaterialType(): string {
    if (!this.m) return this.customOption;
    const index = MATERIAL_TYPE_PRESETS.findIndex(
      (p) => this.languageService.translate(p.nameKey) === this.m!.materialName
        && this.languageService.translate(p.unitKey) === this.m!.unit
    );
    return index >= 0 ? String(index) : this.customOption;
  }

  onMaterialTypeChange(value: string): void {
    if (value === this.customOption) return;
    const preset = this.materialTypePresets[Number(value)];
    this.form.patchValue({ materialName: preset.name, unit: preset.unit });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.getRawValue();
    const result: SavePrivateWorkMaterial = {
      materialName: value.materialName,
      vendorName: value.vendorName,
      unit: value.unit,
      quantity: value.quantity,
      rate: value.rate,
      paymentDate: toIsoDate(value.paymentDate)!
    };
    this.dialogRef.close(result);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
