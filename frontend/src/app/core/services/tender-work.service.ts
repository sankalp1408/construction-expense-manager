import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { SaveTenderRaBill, SaveTenderWork, TenderRaBill, TenderWork } from '../models/tender-work.model';

@Injectable({ providedIn: 'root' })
export class TenderWorkService {
  private readonly baseUrl = `${environment.apiBaseUrl}/tender-works`;

  constructor(private readonly http: HttpClient) {}

  getAll(): Observable<TenderWork[]> {
    return this.http.get<TenderWork[]>(this.baseUrl);
  }

  getById(id: number): Observable<TenderWork> {
    return this.http.get<TenderWork>(`${this.baseUrl}/${id}`);
  }

  create(dto: SaveTenderWork): Observable<TenderWork> {
    return this.http.post<TenderWork>(this.baseUrl, dto);
  }

  update(id: number, dto: SaveTenderWork): Observable<TenderWork> {
    return this.http.put<TenderWork>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  getRaBills(tenderWorkId: number): Observable<TenderRaBill[]> {
    return this.http.get<TenderRaBill[]>(`${this.baseUrl}/${tenderWorkId}/ra-bills`);
  }

  addRaBill(tenderWorkId: number, dto: SaveTenderRaBill): Observable<TenderRaBill> {
    return this.http.post<TenderRaBill>(`${this.baseUrl}/${tenderWorkId}/ra-bills`, dto);
  }

  updateRaBill(tenderWorkId: number, raBillId: number, dto: SaveTenderRaBill): Observable<TenderRaBill> {
    return this.http.put<TenderRaBill>(`${this.baseUrl}/${tenderWorkId}/ra-bills/${raBillId}`, dto);
  }

  deleteRaBill(tenderWorkId: number, raBillId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${tenderWorkId}/ra-bills/${raBillId}`);
  }
}
