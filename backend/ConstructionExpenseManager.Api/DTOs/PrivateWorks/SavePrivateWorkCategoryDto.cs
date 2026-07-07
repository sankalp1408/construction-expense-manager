using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class SavePrivateWorkCategoryDto
{
    [Required, MaxLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [MaxLength(150)]
    public string WorkerName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string RateBasis { get; set; } = string.Empty;

    public decimal AgreedTotalAmount { get; set; }
}
