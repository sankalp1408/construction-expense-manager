namespace ConstructionExpenseManager.Api.DTOs.Dashboard;

public class DashboardSummaryDto
{
    public int TenderWorkCount { get; set; }
    public int CommissionWorkCount { get; set; }
    public int PrivateWorkCount { get; set; }
    public int TotalWorkCount { get; set; }

    public decimal TotalTenderProfit { get; set; }
    public decimal TotalCommissionEarned { get; set; }
    public decimal TotalPrivatePendingPayments { get; set; }

    public decimal TotalTenderBilledAmount { get; set; }
    public decimal TotalPrivateWorkAmount { get; set; }

    // Detailed section breakdowns (always computed from the date range only,
    // independent of the WorkType filter, since they already separate by type).
    public TenderSummaryDto TenderSummary { get; set; } = new();
    public CommissionSummaryDto CommissionSummary { get; set; } = new();
    public PrivateWorkSummaryDto PrivateWorkSummary { get; set; } = new();
    public OverallSummaryDto OverallSummary { get; set; } = new();
}
