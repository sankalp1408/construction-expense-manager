namespace ConstructionExpenseManager.Api.Models;

public class PrivateWorkCategory
{
    public int Id { get; set; }
    public int PrivateWorkId { get; set; }
    public PrivateWork? PrivateWork { get; set; }

    public string CategoryName { get; set; } = string.Empty;
    public string WorkerName { get; set; } = string.Empty;
    public string RateBasis { get; set; } = string.Empty;
    public decimal AgreedTotalAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<PrivateWorkCategoryPayment> Payments { get; set; } = new List<PrivateWorkCategoryPayment>();
}
