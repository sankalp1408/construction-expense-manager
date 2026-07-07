export interface TenderWork {
  id: number;
  tenderName: string;
  nameOfWork: string;
  tenderAmount: number;
  tenderFee: number;
  tenderEMD: number;
  tenderFilingAmount: number;
  gstTotal: number;
  billedGst: number;
  extraGstBill: number;
  workExpenditure: number;
  securityDepositPercent: number;
  officeProtocolPercent: number;
  corporatorName: string;
  corporatorProtocolPercent: number;
  gstBillCommission: number;

  // Calculated: sum of all RA Bill "Billed Amount" entries for this tender.
  billedAmount: number;
  gstFiling: number;
  securityDepositAmount: number;
  officeProtocolAmount: number;
  corporatorProtocolAmount: number;
  profit: number;

  raBillCount: number;
  totalRaBilledAmount: number;

  createdAt: string;
  updatedAt?: string;
}

export type SaveTenderWork = Omit<TenderWork,
  'id' | 'billedAmount' | 'gstFiling' | 'securityDepositAmount' | 'officeProtocolAmount' | 'corporatorProtocolAmount'
  | 'profit' | 'raBillCount' | 'totalRaBilledAmount' | 'createdAt' | 'updatedAt'>;

export interface TenderRaBill {
  id: number;
  tenderWorkId: number;
  raBillNumber: string;
  billDate: string;
  billedAmount: number;
  corporatorCommissionPercent: number;
  corporatorCommissionAmount: number;
  officerCommissionPercent: number;
  officerCommissionAmount: number;
  remarks?: string;
  createdAt: string;
}

export type SaveTenderRaBill = Omit<TenderRaBill,
  'id' | 'tenderWorkId' | 'corporatorCommissionAmount' | 'officerCommissionAmount' | 'createdAt'>;
