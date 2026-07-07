namespace ConstructionExpenseManager.Api.DTOs.Reports;

public class VendorReportDto
{
    public string VendorName { get; set; } = string.Empty;
    public decimal TotalGstBillAmount { get; set; }
    public decimal TotalPaidToVendor { get; set; }
    public decimal TotalCommission { get; set; }
    public decimal TotalCashReturned { get; set; }
}
