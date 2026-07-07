namespace ConstructionExpenseManager.Api.Models;

public class TenderRaBill
{
    public int Id { get; set; }
    public int TenderWorkId { get; set; }
    public TenderWork? TenderWork { get; set; }

    public string RaBillNumber { get; set; } = string.Empty;
    public DateTime BillDate { get; set; }
    public decimal BilledAmount { get; set; }
    public decimal CorporatorCommissionPercent { get; set; } = 10;
    public decimal OfficerCommissionPercent { get; set; } = 8;
    public string? Remarks { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
