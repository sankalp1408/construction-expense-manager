namespace ConstructionExpenseManager.Api.DTOs.Dashboard;

public class CommissionSummaryDto
{
    public decimal TotalTenderAmount { get; set; }
    public decimal TotalBillReceived { get; set; }
    public decimal CommissionAmount { get; set; }
    public decimal TotalGst { get; set; }
    public decimal TotalBilledGst { get; set; }
    public decimal ExtraGstTaken { get; set; }
    public decimal GstFiling { get; set; }
    public decimal TotalCommissionForGst { get; set; }
    public decimal Profit { get; set; }
}
