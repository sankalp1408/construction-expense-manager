namespace ConstructionExpenseManager.Api.DTOs.Dashboard;

public class OverallSummaryDto
{
    public decimal TotalPayment { get; set; }
    public decimal TotalPaymentReceived { get; set; }
    public decimal TotalPaymentBalance { get; set; }
    public decimal TotalGst { get; set; }
    public decimal TotalExtraGstTaken { get; set; }
    public decimal TotalGstCommission { get; set; }
    public decimal TotalGstFiling { get; set; }
    public decimal TotalProfit { get; set; }
}
