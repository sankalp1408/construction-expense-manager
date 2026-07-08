namespace ConstructionExpenseManager.Api.Models;

public class TenderWork
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
    public decimal SecurityDepositPercent { get; set; } = 10;
    public string CorporatorName { get; set; } = string.Empty;
    public decimal CorporatorProtocolPercent { get; set; } = 10;
    public decimal GstBillCommission { get; set; }

    public int CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<TenderRaBill> RaBills { get; set; } = new List<TenderRaBill>();
}
