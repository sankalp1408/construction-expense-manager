using ConstructionExpenseManager.Api.DTOs.Dashboard;

namespace ConstructionExpenseManager.Api.Services;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync(DashboardQueryDto query);
}
