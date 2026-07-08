using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.PrivateWorks;

// The whole entry (date + all labour rows) is created/replaced as one unit, matching
// the one-form "record a day's labour" UX — there's no separate row-level CRUD.
public class SavePrivateWorkDepartmentalLabourDto
{
    [Required]
    public DateTime LabourDate { get; set; }

    public List<SavePrivateWorkDepartmentalLabourRowDto> Rows { get; set; } = new();
}
