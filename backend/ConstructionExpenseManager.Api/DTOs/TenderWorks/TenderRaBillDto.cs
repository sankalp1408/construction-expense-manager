namespace ConstructionExpenseManager.Api.DTOs.TenderWorks;

public class TenderRaBillDto
{
    public int Id { get; set; }
    public int TenderWorkId { get; set; }
    public string RaBillNumber { get; set; } = string.Empty;
    public DateTime BillDate { get; set; }
    public decimal BilledAmount { get; set; }
    public decimal CorporatorCommissionPercent { get; set; }
    public decimal CorporatorCommissionAmount { get; set; }
    public decimal OfficerCommissionPercent { get; set; }
    public decimal OfficerCommissionAmount { get; set; }
    public string? Remarks { get; set; }
    public DateTime CreatedAt { get; set; }
}
