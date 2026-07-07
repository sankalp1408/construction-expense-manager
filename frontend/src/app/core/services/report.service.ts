import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ReportQuery, TenderReport } from '../models/report.model';

@Injectable({ providedIn: 'root' })
export class ReportService {
  private readonly baseUrl = `${environment.apiBaseUrl}/reports`;

  constructor(private readonly http: HttpClient) {}

  getTenderReport(query: ReportQuery): Observable<TenderReport> {
    const params = new HttpParams()
      .set('fyStartYear', query.fyStartYear)
      .set('period', query.period);

    return this.http.get<TenderReport>(`${this.baseUrl}/tender`, { params });
  }
}
