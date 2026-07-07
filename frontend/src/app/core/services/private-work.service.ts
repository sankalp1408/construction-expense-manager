import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  PrivateWork, PrivateWorkCategory, PrivateWorkCategoryPayment, PrivateWorkMaterial, PrivateWorkMilestone,
  SavePrivateWork, SavePrivateWorkCategory, SavePrivateWorkCategoryPayment, SavePrivateWorkMaterial,
  SavePrivateWorkMilestone
} from '../models/private-work.model';

@Injectable({ providedIn: 'root' })
export class PrivateWorkService {
  private readonly baseUrl = `${environment.apiBaseUrl}/private-works`;

  constructor(private readonly http: HttpClient) {}

  getAll(): Observable<PrivateWork[]> {
    return this.http.get<PrivateWork[]>(this.baseUrl);
  }

  getById(id: number): Observable<PrivateWork> {
    return this.http.get<PrivateWork>(`${this.baseUrl}/${id}`);
  }

  create(dto: SavePrivateWork): Observable<PrivateWork> {
    return this.http.post<PrivateWork>(this.baseUrl, dto);
  }

  update(id: number, dto: SavePrivateWork): Observable<PrivateWork> {
    return this.http.put<PrivateWork>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  // ----- Milestones -----

  addMilestone(privateWorkId: number, dto: SavePrivateWorkMilestone): Observable<PrivateWorkMilestone> {
    return this.http.post<PrivateWorkMilestone>(`${this.baseUrl}/${privateWorkId}/milestones`, dto);
  }

  updateMilestone(privateWorkId: number, milestoneId: number, dto: SavePrivateWorkMilestone): Observable<PrivateWorkMilestone> {
    return this.http.put<PrivateWorkMilestone>(`${this.baseUrl}/${privateWorkId}/milestones/${milestoneId}`, dto);
  }

  deleteMilestone(privateWorkId: number, milestoneId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${privateWorkId}/milestones/${milestoneId}`);
  }

  // ----- Categories -----

  addCategory(privateWorkId: number, dto: SavePrivateWorkCategory): Observable<PrivateWorkCategory> {
    return this.http.post<PrivateWorkCategory>(`${this.baseUrl}/${privateWorkId}/categories`, dto);
  }

  updateCategory(privateWorkId: number, categoryId: number, dto: SavePrivateWorkCategory): Observable<PrivateWorkCategory> {
    return this.http.put<PrivateWorkCategory>(`${this.baseUrl}/${privateWorkId}/categories/${categoryId}`, dto);
  }

  deleteCategory(privateWorkId: number, categoryId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${privateWorkId}/categories/${categoryId}`);
  }

  // ----- Category Payments -----

  addCategoryPayment(privateWorkId: number, categoryId: number, dto: SavePrivateWorkCategoryPayment): Observable<PrivateWorkCategoryPayment> {
    return this.http.post<PrivateWorkCategoryPayment>(`${this.baseUrl}/${privateWorkId}/categories/${categoryId}/payments`, dto);
  }

  updateCategoryPayment(privateWorkId: number, categoryId: number, paymentId: number, dto: SavePrivateWorkCategoryPayment): Observable<PrivateWorkCategoryPayment> {
    return this.http.put<PrivateWorkCategoryPayment>(`${this.baseUrl}/${privateWorkId}/categories/${categoryId}/payments/${paymentId}`, dto);
  }

  deleteCategoryPayment(privateWorkId: number, categoryId: number, paymentId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${privateWorkId}/categories/${categoryId}/payments/${paymentId}`);
  }

  // ----- Materials -----

  addMaterial(privateWorkId: number, dto: SavePrivateWorkMaterial): Observable<PrivateWorkMaterial> {
    return this.http.post<PrivateWorkMaterial>(`${this.baseUrl}/${privateWorkId}/materials`, dto);
  }

  updateMaterial(privateWorkId: number, materialId: number, dto: SavePrivateWorkMaterial): Observable<PrivateWorkMaterial> {
    return this.http.put<PrivateWorkMaterial>(`${this.baseUrl}/${privateWorkId}/materials/${materialId}`, dto);
  }

  deleteMaterial(privateWorkId: number, materialId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${privateWorkId}/materials/${materialId}`);
  }
}
