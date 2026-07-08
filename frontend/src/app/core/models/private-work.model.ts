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
  unit: string;
  quantity: number;
  rate: number;
  amount: number;
  paymentDate: string;
}

export type SavePrivateWorkMaterial = Omit<PrivateWorkMaterial, 'id' | 'privateWorkId' | 'amount'>;

// Material Type presets for the Material Payment form: each fixes the unit that
// material is tracked in (e.g. Steel is always Kg). "Custom" (no preset) lets the
// user name their own material and unit.
export const MATERIAL_TYPE_PRESETS: { nameKey: string; unitKey: string }[] = [
  { nameKey: 'material.presetSteel', unitKey: 'material.unitKg' },
  { nameKey: 'material.presetCrushSand', unitKey: 'material.unitBrass' },
  { nameKey: 'material.presetAggregate', unitKey: 'material.unitBrass' },
  { nameKey: 'material.presetBricks', unitKey: 'material.unitNumbers' },
  { nameKey: 'material.presetTiles', unitKey: 'material.unitSqft' },
  { nameKey: 'material.presetCement', unitKey: 'material.unitBags' }
];

export interface PrivateWorkDepartmentalLabourRow {
  id: number;
  labourType: string;
  count: number;
  rate: number;
  subtotal: number;
}

export type SavePrivateWorkDepartmentalLabourRow = Omit<PrivateWorkDepartmentalLabourRow, 'id' | 'subtotal'>;

export interface PrivateWorkDepartmentalLabour {
  id: number;
  privateWorkId: number;
  labourDate: string;
  rows: PrivateWorkDepartmentalLabourRow[];
  total: number;
  createdAt: string;
}

export interface SavePrivateWorkDepartmentalLabour {
  labourDate: string;
  rows: SavePrivateWorkDepartmentalLabourRow[];
}

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
  totalDepartmentalLabour: number;

  totalReceived: number;
  pendingToReceive: number;
  totalUsed: number;
  inHandAmount: number;

  milestones: PrivateWorkMilestone[];
  categories: PrivateWorkCategory[];
  materials: PrivateWorkMaterial[];
  departmentalLabours: PrivateWorkDepartmentalLabour[];

  createdAt: string;
  updatedAt?: string;
}

export type SavePrivateWork = Pick<PrivateWork, 'clientName' | 'workDescription' | 'areaSqft' | 'ratePerSqft'>;

// Common worker/vendor category presets (translation keys) — users can still add custom ones.
export const DEFAULT_CATEGORY_PRESET_KEYS = [
  'category.presetCarpenter', 'category.presetBrickWork', 'category.presetPlaster',
  'category.presetTilesWork', 'category.presetEarthWork'
];
