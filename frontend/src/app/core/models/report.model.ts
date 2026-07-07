export type ReportPeriod = 'Q1' | 'Q2' | 'Q3' | 'Q4' | 'H1' | 'H2' | 'Yearly';

export interface ReportQuery {
  fyStartYear: number;
  period: ReportPeriod;
}

export interface VendorReport {
  vendorName: string;
  totalGstBillAmount: number;
  totalPaidToVendor: number;
  totalCommission: number;
  totalCashReturned: number;
}

export interface TenderReport {
  periodLabel: string;
  fromDate: string;
  toDate: string;

  totalTurnover: number;
  totalGst: number;
  totalVendorGstBilled: number;
  totalVendorCommission: number;
  totalGstFiled: number;

  vendorReports: VendorReport[];
}
