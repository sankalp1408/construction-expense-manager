export interface CommissionWork {
  id: number;
  workName: string;
  partyName: string;
  tenderWorkAmount: number;
  commissionPercent: number;
  gstAmount: number;
  billedGst: number;
  extraGstBill: number;
  gstBillCommission: number;

  // Calculated
  commissionAmount: number;
  gstFiling: number;

  createdAt: string;
  updatedAt?: string;
}

export type SaveCommissionWork = Omit<CommissionWork,
  'id' | 'commissionAmount' | 'gstFiling' | 'createdAt' | 'updatedAt'>;
