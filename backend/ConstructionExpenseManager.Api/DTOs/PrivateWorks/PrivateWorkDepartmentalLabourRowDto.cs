namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class PrivateWorkDepartmentalLabourRowDto
{
    public int Id { get; set; }
    public string LabourType { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Rate { get; set; }
    public decimal Subtotal { get; set; }
}
