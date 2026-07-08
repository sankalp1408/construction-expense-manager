import { CurrencyPipe, DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog } from '@angular/material/dialog';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { AuthService } from '../../../core/services/auth.service';
import { PrivateWorkService } from '../../../core/services/private-work.service';
import {
  PrivateWork, PrivateWorkCategory, PrivateWorkCategoryPayment, PrivateWorkDepartmentalLabour, PrivateWorkMaterial,
  PrivateWorkMilestone
} from '../../../core/models/private-work.model';
import { PrivateWorkForm, PrivateWorkFormDialogData } from '../private-work-form/private-work-form';
import { MilestoneForm, MilestoneFormDialogData } from '../milestone-form/milestone-form';
import { CategoryForm, CategoryFormDialogData } from '../category-form/category-form';
import { CategoryPaymentForm, CategoryPaymentFormDialogData } from '../category-payment-form/category-payment-form';
import { MaterialForm, MaterialFormDialogData } from '../material-form/material-form';
import { DepartmentalLabourForm, DepartmentalLabourFormDialogData } from '../departmental-labour-form/departmental-labour-form';
import { TranslatePipe } from '../../../shared/pipes/translate.pipe';
import { LanguageService } from '../../../core/services/language.service';
import { PulseOnChangeDirective } from '../../../shared/directives/pulse-on-change.directive';

@Component({
  selector: 'app-private-work-detail',
  imports: [
    CurrencyPipe, DatePipe, MatCardModule, MatButtonModule, MatIconModule, MatTableModule, MatChipsModule, MatExpansionModule,
    TranslatePipe, PulseOnChangeDirective
  ],
  templateUrl: './private-work-detail.html',
  styleUrl: './private-work-detail.scss'
})
export class PrivateWorkDetail {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly service = inject(PrivateWorkService);
  private readonly dialog = inject(MatDialog);
  private readonly authService = inject(AuthService);
  private readonly languageService = inject(LanguageService);

  readonly isSuperAdmin = this.authService.isSuperAdmin;
  readonly work = signal<PrivateWork | null>(null);

  readonly milestoneColumns = ['stageName', 'percentOfTotal', 'amount', 'paidAmount', 'status', 'actions'];
  readonly paymentColumns = ['paymentDate', 'amount', 'remarks', 'actions'];
  readonly materialColumns = ['materialName', 'vendorName', 'quantity', 'rate', 'amount', 'paymentDate', 'actions'];
  readonly departmentalLabourRowColumns = ['labourType', 'count', 'rate', 'subtotal'];

  private readonly workId = Number(this.route.snapshot.paramMap.get('id'));

  constructor() {
    this.load();
  }

  load(): void {
    this.service.getById(this.workId).subscribe((w) => this.work.set(w));
  }

  goBack(): void {
    this.router.navigate(['/private-works']);
  }

  // ----- Work header -----

  editWork(): void {
    const current = this.work();
    if (!current) return;

    const ref = this.dialog.open<PrivateWorkForm, PrivateWorkFormDialogData>(PrivateWorkForm, { data: { work: current } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.update(this.workId, result).subscribe(() => this.load());
    });
  }

  deleteWork(): void {
    if (!confirm(this.languageService.translate('privateWork.confirmDeleteWork'))) return;
    this.service.delete(this.workId).subscribe(() => this.router.navigate(['/private-works']));
  }

  // ----- Milestones -----

  addMilestone(): void {
    const nextSortOrder = (this.work()?.milestones.length ?? 0);
    const totalContractAmount = this.work()?.totalAmount ?? 0;
    const ref = this.dialog.open<MilestoneForm, MilestoneFormDialogData>(MilestoneForm, {
      data: { milestone: null, nextSortOrder, totalContractAmount }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.addMilestone(this.workId, result).subscribe(() => this.load());
    });
  }

  editMilestone(milestone: PrivateWorkMilestone): void {
    const totalContractAmount = this.work()?.totalAmount ?? 0;
    const ref = this.dialog.open<MilestoneForm, MilestoneFormDialogData>(MilestoneForm, {
      data: { milestone, nextSortOrder: milestone.sortOrder, totalContractAmount }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.updateMilestone(this.workId, milestone.id, result).subscribe(() => this.load());
    });
  }

  deleteMilestone(milestone: PrivateWorkMilestone): void {
    if (!confirm(this.languageService.translate('privateWork.confirmDeleteMilestone'))) return;
    this.service.deleteMilestone(this.workId, milestone.id).subscribe(() => this.load());
  }

  // ----- Categories -----

  addCategory(): void {
    const ref = this.dialog.open<CategoryForm, CategoryFormDialogData>(CategoryForm, { data: { category: null } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.addCategory(this.workId, result).subscribe(() => this.load());
    });
  }

  editCategory(category: PrivateWorkCategory): void {
    const ref = this.dialog.open<CategoryForm, CategoryFormDialogData>(CategoryForm, { data: { category } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.updateCategory(this.workId, category.id, result).subscribe(() => this.load());
    });
  }

  deleteCategory(category: PrivateWorkCategory): void {
    if (!confirm(this.languageService.translate('privateWork.confirmDeleteCategory', { name: category.categoryName }))) return;
    this.service.deleteCategory(this.workId, category.id).subscribe(() => this.load());
  }

  // ----- Category Payments -----

  addCategoryPayment(category: PrivateWorkCategory): void {
    const ref = this.dialog.open<CategoryPaymentForm, CategoryPaymentFormDialogData>(CategoryPaymentForm, {
      data: { payment: null }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.addCategoryPayment(this.workId, category.id, result).subscribe(() => this.load());
    });
  }

  editCategoryPayment(category: PrivateWorkCategory, payment: PrivateWorkCategoryPayment): void {
    const ref = this.dialog.open<CategoryPaymentForm, CategoryPaymentFormDialogData>(CategoryPaymentForm, {
      data: { payment }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.updateCategoryPayment(this.workId, category.id, payment.id, result).subscribe(() => this.load());
    });
  }

  deleteCategoryPayment(category: PrivateWorkCategory, payment: PrivateWorkCategoryPayment): void {
    if (!confirm(this.languageService.translate('privateWork.confirmDeletePayment'))) return;
    this.service.deleteCategoryPayment(this.workId, category.id, payment.id).subscribe(() => this.load());
  }

  // ----- Materials -----

  addMaterial(): void {
    const ref = this.dialog.open<MaterialForm, MaterialFormDialogData>(MaterialForm, { data: { material: null } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.addMaterial(this.workId, result).subscribe(() => this.load());
    });
  }

  editMaterial(material: PrivateWorkMaterial): void {
    const ref = this.dialog.open<MaterialForm, MaterialFormDialogData>(MaterialForm, { data: { material } });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.updateMaterial(this.workId, material.id, result).subscribe(() => this.load());
    });
  }

  deleteMaterial(material: PrivateWorkMaterial): void {
    if (!confirm(this.languageService.translate('privateWork.confirmDeleteMaterial'))) return;
    this.service.deleteMaterial(this.workId, material.id).subscribe(() => this.load());
  }

  // ----- Departmental Labour -----

  addDepartmentalLabour(): void {
    const ref = this.dialog.open<DepartmentalLabourForm, DepartmentalLabourFormDialogData>(DepartmentalLabourForm, {
      data: { entry: null }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.addDepartmentalLabour(this.workId, result).subscribe(() => this.load());
    });
  }

  editDepartmentalLabour(entry: PrivateWorkDepartmentalLabour): void {
    const ref = this.dialog.open<DepartmentalLabourForm, DepartmentalLabourFormDialogData>(DepartmentalLabourForm, {
      data: { entry }
    });
    ref.afterClosed().subscribe((result) => {
      if (!result) return;
      this.service.updateDepartmentalLabour(this.workId, entry.id, result).subscribe(() => this.load());
    });
  }

  deleteDepartmentalLabour(entry: PrivateWorkDepartmentalLabour): void {
    if (!confirm(this.languageService.translate('privateWork.confirmDeleteDepartmentalLabour'))) return;
    this.service.deleteDepartmentalLabour(this.workId, entry.id).subscribe(() => this.load());
  }
}
