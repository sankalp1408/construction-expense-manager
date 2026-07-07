namespace ConstructionExpenseManager.Api.DTOs.CommissionWorks;

public class CommissionWorkDto
{
    public int Id { get; set; }
    public string WorkName { get; set; } = string.Empty;
    public string PartyName { get; set; } = string.Empty;
    public decimal TenderWorkAmount { get; set; }
    public decimal CommissionPercent { get; set; }
    public decimal GstAmount { get; set; }
    public decimal BilledGst { get; set; }
    public decimal ExtraGstBill { get; set; }
    public decimal GstBillCommission { get; set; }

    // Calculated
    public decimal CommissionAmount { get; set; }
    public decimal GstFiling { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
