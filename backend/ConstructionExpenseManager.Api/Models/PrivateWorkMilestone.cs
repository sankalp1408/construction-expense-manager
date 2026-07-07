namespace ConstructionExpenseManager.Api.Models;

public class PrivateWorkMilestone
{
    public int Id { get; set; }
    public int PrivateWorkId { get; set; }
    public PrivateWork? PrivateWork { get; set; }

    public string StageName { get; set; } = string.Empty;
    public decimal PercentOfTotal { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime? PaidDate { get; set; }
    public MilestoneStatus Status { get; set; } = MilestoneStatus.Pending;
    public int SortOrder { get; set; }
}
