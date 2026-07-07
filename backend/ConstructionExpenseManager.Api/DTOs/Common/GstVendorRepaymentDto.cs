namespace ConstructionExpenseManager.Api.DTOs.Common;

public class GstVendorRepaymentDto
{
    public int Id { get; set; }
    public int GstVendorEntryId { get; set; }
    public DateTime ReceivedDate { get; set; }
    public decimal AmountReceived { get; set; }
    public string Mode { get; set; } = string.Empty;
}
