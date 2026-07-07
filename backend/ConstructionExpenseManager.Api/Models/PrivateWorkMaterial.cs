namespace ConstructionExpenseManager.Api.Models;

public class PrivateWorkMaterial
{
    public int Id { get; set; }
    public int PrivateWorkId { get; set; }
    public PrivateWork? PrivateWork { get; set; }

    public string MaterialName { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
