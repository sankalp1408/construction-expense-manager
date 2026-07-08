namespace ConstructionExpenseManager.Api.Models;

// One day's departmental labour entry for a Private Work — a date plus multiple
// labour-type rows (e.g. Mistry, Labour) each at their own count/rate.
public class PrivateWorkDepartmentalLabour
{
    public int Id { get; set; }
    public int PrivateWorkId { get; set; }
    public PrivateWork? PrivateWork { get; set; }

    public DateTime LabourDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<PrivateWorkDepartmentalLabourRow> Rows { get; set; } = new List<PrivateWorkDepartmentalLabourRow>();
}
