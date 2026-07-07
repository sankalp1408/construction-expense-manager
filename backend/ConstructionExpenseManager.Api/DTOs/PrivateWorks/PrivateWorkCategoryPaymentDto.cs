namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class PrivateWorkCategoryPaymentDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string? Remarks { get; set; }
}
