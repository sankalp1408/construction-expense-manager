export type GstVendorRepaymentMode = 'Cash' | 'GPay' | 'BankTransfer' | 'Other';

export interface GstVendorRepayment {
  id: number;
  gstVendorEntryId: number;
  receivedDate: string;
  amountReceived: number;
  mode: GstVendorRepaymentMode;
}

export type SaveGstVendorRepayment = Omit<GstVendorRepayment, 'id' | 'gstVendorEntryId'>;

export interface GstVendorEntry {
  id: number;
  workId: number;
  vendorName: string;
  gstBillAmount: number;
  sentDate: string;
  commissionPercent: number;

  // Calculated
  commissionAmount: number;
  netPayable: number;
  totalReceivedSoFar: number;
  pendingAmount: number;

  repayments: GstVendorRepayment[];
  createdAt: string;
}

export type SaveGstVendorEntry = Omit<GstVendorEntry,
  'id' | 'workId' | 'commissionAmount' | 'netPayable' | 'totalReceivedSoFar' | 'pendingAmount' | 'repayments' | 'createdAt'>;

export interface GstVendorLedger {
  entries: GstVendorEntry[];
  totalGstBilled: number;
  totalCommissionAmount: number;
  totalNetPayable: number;
  totalReceivedBack: number;
  totalPendingFromVendors: number;
}
