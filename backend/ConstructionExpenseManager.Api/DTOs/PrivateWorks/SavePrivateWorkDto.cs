using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class SavePrivateWorkDto
{
    [Required, MaxLength(150)]
    public string ClientName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string WorkDescription { get; set; } = string.Empty;

    public decimal AreaSqft { get; set; }
    public decimal RatePerSqft { get; set; }
}
