using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.TenderWorks;

// Used for both create and update — a tender work is one flat form either way.
public class SaveTenderWorkDto
{
    [Required, MaxLength(200)]
    public string TenderName { get; set; } = string.Empty;

    [Required, MaxLength(300)]
    public string NameOfWork { get; set; } = string.Empty;

    public decimal TenderAmount { get; set; }
    public decimal TenderFee { get; set; }
    public decimal TenderEMD { get; set; }
    public decimal TenderFilingAmount { get; set; }
    public decimal GstTotal { get; set; }
    public decimal BilledGst { get; set; }
    public decimal ExtraGstBill { get; set; }
    public decimal WorkExpenditure { get; set; }
    public decimal SecurityDepositPercent { get; set; } = 10;
    public string CorporatorName { get; set; } = string.Empty;
    public decimal CorporatorProtocolPercent { get; set; } = 10;
    public decimal GstBillCommission { get; set; }
}
