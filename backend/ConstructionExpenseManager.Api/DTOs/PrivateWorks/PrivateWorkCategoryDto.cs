namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class PrivateWorkCategoryDto
{
    public int Id { get; set; }
    public int PrivateWorkId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string WorkerName { get; set; } = string.Empty;
    public string RateBasis { get; set; } = string.Empty;
    public decimal AgreedTotalAmount { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal RemainingAmount { get; set; }
    public List<PrivateWorkCategoryPaymentDto> Payments { get; set; } = new();
}
