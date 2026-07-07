export interface TenderSummary {
  totalTenderAmount: number;
  totalBillReceived: number;
  totalExpense: number;
  totalGst: number;
  totalBilledGst: number;
  extraGstTaken: number;
  gstFiling: number;
  totalCommissionForGst: number;
  profit: number;
}

export interface CommissionSummary {
  totalTenderAmount: number;
  totalBillReceived: number;
  commissionAmount: number;
  totalGst: number;
  totalBilledGst: number;
  extraGstTaken: number;
  gstFiling: number;
  totalCommissionForGst: number;
  profit: number;
}

export interface PrivateWorkSummary {
  totalWorkAmount: number;
  paymentReceived: number;
  totalExpense: number;
  profit: number;
}

export interface OverallSummary {
  totalPayment: number;
  totalPaymentReceived: number;
  totalPaymentBalance: number;
  totalGst: number;
  totalExtraGstTaken: number;
  totalGstCommission: number;
  totalGstFiling: number;
  totalProfit: number;
}

export interface DashboardSummary {
  tenderWorkCount: number;
  commissionWorkCount: number;
  privateWorkCount: number;
  totalWorkCount: number;

  totalTenderProfit: number;
  totalCommissionEarned: number;
  totalPrivatePendingPayments: number;

  totalTenderBilledAmount: number;
  totalPrivateWorkAmount: number;

  tenderSummary: TenderSummary;
  commissionSummary: CommissionSummary;
  privateWorkSummary: PrivateWorkSummary;
  overallSummary: OverallSummary;
}

export interface DashboardQuery {
  fromDate?: string;
  toDate?: string;
}
