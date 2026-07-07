using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.Common;

public class SaveGstVendorEntryDto
{
    [Required, MaxLength(150)]
    public string VendorName { get; set; } = string.Empty;

    public decimal GstBillAmount { get; set; }
    public decimal CommissionPercent { get; set; }

    [Required]
    public DateTime SentDate { get; set; }
}
