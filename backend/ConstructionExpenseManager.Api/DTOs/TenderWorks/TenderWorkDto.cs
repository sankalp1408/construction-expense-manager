namespace ConstructionExpenseManager.Api.DTOs.TenderWorks;

public class TenderWorkDto
{
    public int Id { get; set; }
    public string TenderName { get; set; } = string.Empty;
    public string NameOfWork { get; set; } = string.Empty;
    public decimal TenderAmount { get; set; }
    public decimal TenderFee { get; set; }
    public decimal TenderEMD { get; set; }
    public decimal TenderFilingAmount { get; set; }
    public decimal GstTotal { get; set; }
    public decimal BilledGst { get; set; }
    public decimal ExtraGstBill { get; set; }
    public decimal WorkExpenditure { get; set; }
    public decimal SecurityDepositPercent { get; set; }
    public string CorporatorName { get; set; } = string.Empty;
    public decimal CorporatorProtocolPercent { get; set; }
    public decimal GstBillCommission { get; set; }

    // Calculated fields
    public decimal BilledAmount { get; set; }
    public decimal GstFiling { get; set; }
    public decimal SecurityDepositAmount { get; set; }
    public decimal OfficeProtocolAmount { get; set; }
    public decimal CorporatorProtocolAmount { get; set; }
    public decimal Profit { get; set; }

    public int RaBillCount { get; set; }
    public decimal TotalRaBilledAmount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
