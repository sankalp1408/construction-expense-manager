namespace ConstructionExpenseManager.Api.Models;

// A single partial repayment made by a vendor against a GstVendorEntry.
public class GstVendorRepayment
{
    public int Id { get; set; }
    public int GstVendorEntryId { get; set; }
    public GstVendorEntry? GstVendorEntry { get; set; }

    public DateTime ReceivedDate { get; set; }
    public decimal AmountReceived { get; set; }
    public GstVendorRepaymentMode Mode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
