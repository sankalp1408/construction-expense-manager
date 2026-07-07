using ConstructionExpenseManager.Api.DTOs.Reports;
using ConstructionExpenseManager.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionExpenseManager.Api.Controllers;

[ApiController]
[Route("api/reports")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("tender")]
    public async Task<IActionResult> GetTenderReport([FromQuery] ReportQueryDto query) =>
        Ok(await _reportService.GetTenderReportAsync(query));
}
