namespace ConstructionExpenseManager.Api.DTOs.Reports;

public class TenderReportDto
{
    public string PeriodLabel { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public decimal TotalTurnover { get; set; }
    public decimal TotalGst { get; set; }
    public decimal TotalVendorGstBilled { get; set; }
    public decimal TotalVendorCommission { get; set; }
    public decimal TotalGstFiled { get; set; }

    public List<VendorReportDto> VendorReports { get; set; } = new();
}
