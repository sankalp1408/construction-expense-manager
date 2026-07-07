import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CommissionWork, SaveCommissionWork } from '../models/commission-work.model';

@Injectable({ providedIn: 'root' })
export class CommissionWorkService {
  private readonly baseUrl = `${environment.apiBaseUrl}/commission-works`;

  constructor(private readonly http: HttpClient) {}

  getAll(): Observable<CommissionWork[]> {
    return this.http.get<CommissionWork[]>(this.baseUrl);
  }

  getById(id: number): Observable<CommissionWork> {
    return this.http.get<CommissionWork>(`${this.baseUrl}/${id}`);
  }

  create(dto: SaveCommissionWork): Observable<CommissionWork> {
    return this.http.post<CommissionWork>(this.baseUrl, dto);
  }

  update(id: number, dto: SaveCommissionWork): Observable<CommissionWork> {
    return this.http.put<CommissionWork>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
