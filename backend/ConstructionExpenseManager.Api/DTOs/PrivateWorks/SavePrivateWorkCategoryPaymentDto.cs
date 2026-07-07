using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class SavePrivateWorkCategoryPaymentDto
{
    [Required]
    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    [MaxLength(300)]
    public string? Remarks { get; set; }
}
