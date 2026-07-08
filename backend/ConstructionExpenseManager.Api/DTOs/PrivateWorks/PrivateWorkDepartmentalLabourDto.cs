namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class PrivateWorkDepartmentalLabourDto
{
    public int Id { get; set; }
    public int PrivateWorkId { get; set; }
    public DateTime LabourDate { get; set; }
    public List<PrivateWorkDepartmentalLabourRowDto> Rows { get; set; } = new();
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}
