namespace ConstructionExpenseManager.Api.DTOs.Dashboard;

public class PrivateWorkSummaryDto
{
    public decimal TotalWorkAmount { get; set; }
    public decimal PaymentReceived { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Profit { get; set; }
}
