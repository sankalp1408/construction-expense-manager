namespace ConstructionExpenseManager.Api.Models;

// A single labour-type line within a PrivateWorkDepartmentalLabour entry (e.g. "Mistry" x5 @ 750).
public class PrivateWorkDepartmentalLabourRow
{
    public int Id { get; set; }
    public int DepartmentalLabourId { get; set; }
    public PrivateWorkDepartmentalLabour? DepartmentalLabour { get; set; }

    public string LabourType { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Rate { get; set; }
}
