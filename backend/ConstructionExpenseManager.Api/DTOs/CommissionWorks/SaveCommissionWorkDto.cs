using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.CommissionWorks;

public class SaveCommissionWorkDto
{
    [Required, MaxLength(300)]
    public string WorkName { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string PartyName { get; set; } = string.Empty;

    public decimal TenderWorkAmount { get; set; }
    public decimal CommissionPercent { get; set; }
    public decimal GstAmount { get; set; }
    public decimal BilledGst { get; set; }
    public decimal ExtraGstBill { get; set; }
    public decimal GstBillCommission { get; set; }
}
