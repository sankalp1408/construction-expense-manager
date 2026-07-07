namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class PrivateWorkMaterialDto
{
    public int Id { get; set; }
    public int PrivateWorkId { get; set; }
    public string MaterialName { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}
