namespace ConstructionExpenseManager.Api.DTOs.Reports;

public class ReportQueryDto
{
    // Financial year start calendar year, e.g. 2026 means FY 2026-27 (Apr 2026 - Mar 2027).
    public int FyStartYear { get; set; }

    // "Q1" | "Q2" | "Q3" | "Q4" | "H1" | "H2" | "Yearly"
    public string Period { get; set; } = "Yearly";
}
