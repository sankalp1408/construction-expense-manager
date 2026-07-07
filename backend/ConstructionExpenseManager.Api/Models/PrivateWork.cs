namespace ConstructionExpenseManager.Api.Models;

public class PrivateWork
{
    public int Id { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string WorkDescription { get; set; } = string.Empty;
    public decimal AreaSqft { get; set; }
    public decimal RatePerSqft { get; set; }

    public int CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<PrivateWorkMilestone> Milestones { get; set; } = new List<PrivateWorkMilestone>();
    public ICollection<PrivateWorkCategory> Categories { get; set; } = new List<PrivateWorkCategory>();
    public ICollection<PrivateWorkMaterial> Materials { get; set; } = new List<PrivateWorkMaterial>();
}
