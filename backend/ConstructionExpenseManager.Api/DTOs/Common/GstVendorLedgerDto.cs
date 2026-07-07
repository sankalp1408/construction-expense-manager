namespace ConstructionExpenseManager.Api.DTOs.Common;

public class GstVendorLedgerDto
{
    public List<GstVendorEntryDto> Entries { get; set; } = new();
    public decimal TotalGstBilled { get; set; }
    public decimal TotalCommissionAmount { get; set; }
    public decimal TotalNetPayable { get; set; }
    public decimal TotalReceivedBack { get; set; }
    public decimal TotalPendingFromVendors { get; set; }
}
