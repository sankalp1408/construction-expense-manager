using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class SavePrivateWorkMilestoneDto
{
    [Required, MaxLength(150)]
    public string StageName { get; set; } = string.Empty;

    public decimal PercentOfTotal { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime? PaidDate { get; set; }

    [Required]
    public string Status { get; set; } = "Pending";

    public int SortOrder { get; set; }
}
