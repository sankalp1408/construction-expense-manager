using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.TenderWorks;

public class SaveTenderRaBillDto
{
    [Required, MaxLength(50)]
    public string RaBillNumber { get; set; } = string.Empty;

    [Required]
    public DateTime BillDate { get; set; }

    public decimal BilledAmount { get; set; }
    public decimal CorporatorCommissionPercent { get; set; } = 10;
    public decimal OfficerCommissionPercent { get; set; } = 8;

    [MaxLength(500)]
    public string? Remarks { get; set; }
}
