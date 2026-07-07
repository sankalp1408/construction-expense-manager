namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class PrivateWorkDto
{
    public int Id { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string WorkDescription { get; set; } = string.Empty;
    public decimal AreaSqft { get; set; }
    public decimal RatePerSqft { get; set; }
    public decimal TotalAmount { get; set; }

    public decimal TotalMilestonePaid { get; set; }
    public decimal TotalMilestonePending { get; set; }
    public decimal TotalWorkerPaid { get; set; }
    public decimal TotalWorkerRemaining { get; set; }
    public decimal TotalMaterialAmount { get; set; }

    // Money-flow / in-hand summary
    public decimal TotalReceived { get; set; }
    public decimal PendingToReceive { get; set; }
    public decimal TotalUsed { get; set; }
    public decimal InHandAmount { get; set; }

    public List<PrivateWorkMilestoneDto> Milestones { get; set; } = new();
    public List<PrivateWorkCategoryDto> Categories { get; set; } = new();
    public List<PrivateWorkMaterialDto> Materials { get; set; } = new();

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
