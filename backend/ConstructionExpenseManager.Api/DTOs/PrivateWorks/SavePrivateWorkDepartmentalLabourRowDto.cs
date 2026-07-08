using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

public class SavePrivateWorkDepartmentalLabourRowDto
{
    [Required, MaxLength(100)]
    public string LabourType { get; set; } = string.Empty;

    public int Count { get; set; }
    public decimal Rate { get; set; }
}
