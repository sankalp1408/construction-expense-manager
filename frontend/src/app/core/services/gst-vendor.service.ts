import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  GstVendorEntry, GstVendorLedger, GstVendorRepayment, SaveGstVendorEntry, SaveGstVendorRepayment
} from '../models/gst-vendor-entry.model';

export type WorkKind = 'tender-works' | 'commission-works';

// Shared sub-ledger service used by both Tender Works and Commission Works.
@Injectable({ providedIn: 'root' })
export class GstVendorService {
  constructor(private readonly http: HttpClient) {}

  private baseUrl(workKind: WorkKind, workId: number): string {
    return `${environment.apiBaseUrl}/${workKind}/${workId}/gst-vendors`;
  }

  getLedger(workKind: WorkKind, workId: number): Observable<GstVendorLedger> {
    return this.http.get<GstVendorLedger>(this.baseUrl(workKind, workId));
  }

  add(workKind: WorkKind, workId: number, entry: SaveGstVendorEntry): Observable<GstVendorEntry> {
    return this.http.post<GstVendorEntry>(this.baseUrl(workKind, workId), entry);
  }

  update(workKind: WorkKind, workId: number, entryId: number, entry: SaveGstVendorEntry): Observable<GstVendorEntry> {
    return this.http.put<GstVendorEntry>(`${this.baseUrl(workKind, workId)}/${entryId}`, entry);
  }

  delete(workKind: WorkKind, workId: number, entryId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl(workKind, workId)}/${entryId}`);
  }

  addRepayment(workKind: WorkKind, workId: number, entryId: number, repayment: SaveGstVendorRepayment): Observable<GstVendorRepayment> {
    return this.http.post<GstVendorRepayment>(`${this.baseUrl(workKind, workId)}/${entryId}/repayments`, repayment);
  }

  updateRepayment(workKind: WorkKind, workId: number, entryId: number, repaymentId: number, repayment: SaveGstVendorRepayment): Observable<GstVendorRepayment> {
    return this.http.put<GstVendorRepayment>(`${this.baseUrl(workKind, workId)}/${entryId}/repayments/${repaymentId}`, repayment);
  }

  deleteRepayment(workKind: WorkKind, workId: number, entryId: number, repaymentId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl(workKind, workId)}/${entryId}/repayments/${repaymentId}`);
  }
}
