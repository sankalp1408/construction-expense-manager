using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.Common;

public class SaveGstVendorRepaymentDto
{
    [Required]
    public DateTime ReceivedDate { get; set; }

    public decimal AmountReceived { get; set; }

    [Required]
    public string Mode { get; set; } = "Cash";
}
