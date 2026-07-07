namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class PrivateWorkMilestoneDto
{
    public int Id { get; set; }
    public int PrivateWorkId { get; set; }
    public string StageName { get; set; } = string.Empty;
    public decimal PercentOfTotal { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime? PaidDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
