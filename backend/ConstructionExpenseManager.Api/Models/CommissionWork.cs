namespace ConstructionExpenseManager.Api.Models;

public class CommissionWork
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

    public int CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
