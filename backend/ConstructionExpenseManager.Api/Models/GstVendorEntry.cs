namespace ConstructionExpenseManager.Api.Models;

// Shared GST vendor sub-ledger, reused by both TenderWork and CommissionWork
// (WorkType + WorkId acts as a polymorphic reference instead of duplicating the table).
public class GstVendorEntry
{
    public int Id { get; set; }
    public GstWorkType WorkType { get; set; }
    public int WorkId { get; set; }

    public string VendorName { get; set; } = string.Empty;
    public decimal GstBillAmount { get; set; }
    public DateTime SentDate { get; set; }
    public decimal CommissionPercent { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<GstVendorRepayment> Repayments { get; set; } = new List<GstVendorRepayment>();
}
