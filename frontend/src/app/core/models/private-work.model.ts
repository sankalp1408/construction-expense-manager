export type MilestoneStatus = 'Pending' | 'Partial' | 'Paid';

export interface PrivateWorkMilestone {
  id: number;
  privateWorkId: number;
  stageName: string;
  percentOfTotal: number;
  amount: number;
  paidAmount: number;
  paidDate?: string;
  status: MilestoneStatus;
  sortOrder: number;
}

export type SavePrivateWorkMilestone = Omit<PrivateWorkMilestone, 'id' | 'privateWorkId' | 'amount'>;

export interface PrivateWorkCategoryPayment {
  id: number;
  categoryId: number;
  paymentDate: string;
  amount: number;
  remarks?: string;
}

export type SavePrivateWorkCategoryPayment = Omit<PrivateWorkCategoryPayment, 'id' | 'categoryId'>;

export interface PrivateWorkCategory {
  id: number;
  privateWorkId: number;
  categoryName: string;
  workerName: string;
  rateBasis: string;
  agreedTotalAmount: number;
  totalPaid: number;
  remainingAmount: number;
  payments: PrivateWorkCategoryPayment[];
}

export type SavePrivateWorkCategory = Omit<PrivateWorkCategory,
  'id' | 'privateWorkId' | 'totalPaid' | 'remainingAmount' | 'payments'>;

export interface PrivateWorkMaterial {
  id: number;
  privateWorkId: number;
  materialName: string;
  vendorName: string;
  amount: number;
  paymentDate: string;
}

export type SavePrivateWorkMaterial = Omit<PrivateWorkMaterial, 'id' | 'privateWorkId'>;

export interface PrivateWork {
  id: number;
  clientName: string;
  workDescription: string;
  areaSqft: number;
  ratePerSqft: number;
  totalAmount: number;

  totalMilestonePaid: number;
  totalMilestonePending: number;
  totalWorkerPaid: number;
  totalWorkerRemaining: number;
  totalMaterialAmount: number;

  totalReceived: number;
  pendingToReceive: number;
  totalUsed: number;
  inHandAmount: number;

  milestones: PrivateWorkMilestone[];
  categories: PrivateWorkCategory[];
  materials: PrivateWorkMaterial[];

  createdAt: string;
  updatedAt?: string;
}

export type SavePrivateWork = Pick<PrivateWork, 'clientName' | 'workDescription' | 'areaSqft' | 'ratePerSqft'>;

// Common worker/vendor category presets (translation keys) — users can still add custom ones.
export const DEFAULT_CATEGORY_PRESET_KEYS = [
  'category.presetCarpenter', 'category.presetBrickWork', 'category.presetPlaster',
  'category.presetTilesWork', 'category.presetEarthWork'
];
