using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class SavePrivateWorkMaterialDto
{
    [Required, MaxLength(150)]
    public string MaterialName { get; set; } = string.Empty;

    [MaxLength(150)]
    public string VendorName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }
}
