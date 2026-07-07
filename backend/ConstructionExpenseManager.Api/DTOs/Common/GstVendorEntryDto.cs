namespace ConstructionExpenseManager.Api.DTOs.Common;

public class GstVendorEntryDto
{
    public int Id { get; set; }
    public int WorkId { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public decimal GstBillAmount { get; set; }
    public DateTime SentDate { get; set; }
    public decimal CommissionPercent { get; set; }

    // Calculated
    public decimal CommissionAmount { get; set; }
    public decimal NetPayable { get; set; }
    public decimal TotalReceivedSoFar { get; set; }
    public decimal PendingAmount { get; set; }

    public List<GstVendorRepaymentDto> Repayments { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
