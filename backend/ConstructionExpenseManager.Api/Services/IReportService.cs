using ConstructionExpenseManager.Api.DTOs.Reports;

namespace ConstructionExpenseManager.Api.Services;

public interface IReportService
{
    Task<TenderReportDto> GetTenderReportAsync(ReportQueryDto query);
}
