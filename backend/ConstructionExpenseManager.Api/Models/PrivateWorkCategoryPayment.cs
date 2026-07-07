namespace ConstructionExpenseManager.Api.Models;

public class PrivateWorkCategoryPayment
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public PrivateWorkCategory? Category { get; set; }

    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string? Remarks { get; set; }
}
