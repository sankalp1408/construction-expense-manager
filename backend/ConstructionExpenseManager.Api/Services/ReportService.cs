using ConstructionExpenseManager.Api.DTOs.Reports;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Repositories;

namespace ConstructionExpenseManager.Api.Services;

public class ReportService : IReportService
{
    private readonly ITenderWorkRepository _tenderWorkRepository;
    private readonly IGstVendorEntryRepository _gstVendorEntryRepository;

    public ReportService(ITenderWorkRepository tenderWorkRepository, IGstVendorEntryRepository gstVendorEntryRepository)
    {
        _tenderWorkRepository = tenderWorkRepository;
        _gstVendorEntryRepository = gstVendorEntryRepository;
    }

    public async Task<TenderReportDto> GetTenderReportAsync(ReportQueryDto query)
    {
        var (from, to, label) = ResolvePeriod(query.FyStartYear, query.Period);
        var toExclusive = to.AddDays(1);

        var tenderWorks = await _tenderWorkRepository.FindAsync(t => t.CreatedAt >= from && t.CreatedAt < toExclusive);
        var tenderDtos = tenderWorks.Select(TenderWorkService.ToDto).ToList();

        var vendorEntries = await _gstVendorEntryRepository.FindWithRepaymentsAsync(
            g => g.WorkType == GstWorkType.Tender && g.SentDate >= from && g.SentDate < toExclusive);
        var vendorDtos = vendorEntries.Select(GstVendorLedgerService.ToDto).ToList();

        var vendorReports = vendorDtos
            .GroupBy(v => v.VendorName)
            .Select(g => new VendorReportDto
            {
                VendorName = g.Key,
                TotalGstBillAmount = g.Sum(v => v.GstBillAmount),
                TotalPaidToVendor = g.Sum(v => v.NetPayable),
                TotalCommission = g.Sum(v => v.CommissionAmount),
                TotalCashReturned = g.Sum(v => v.TotalReceivedSoFar)
            })
            .OrderByDescending(v => v.TotalGstBillAmount)
            .ToList();

        return new TenderReportDto
        {
            PeriodLabel = label,
            FromDate = from,
            ToDate = to,
            TotalTurnover = tenderDtos.Sum(t => t.BilledAmount),
            TotalGst = tenderDtos.Sum(t => t.GstTotal),
            TotalVendorGstBilled = vendorDtos.Sum(v => v.GstBillAmount),
            TotalVendorCommission = vendorDtos.Sum(v => v.CommissionAmount),
            TotalGstFiled = tenderDtos.Sum(t => t.GstFiling),
            VendorReports = vendorReports
        };
    }

    private static (DateTime From, DateTime To, string Label) ResolvePeriod(int fyStartYear, string period)
    {
        var y = fyStartYear;

        return period.ToUpperInvariant() switch
        {
            "Q1" => (new DateTime(y, 4, 1), new DateTime(y, 6, 30), $"Q1 FY {y}-{(y + 1) % 100:D2} (Apr-Jun)"),
            "Q2" => (new DateTime(y, 7, 1), new DateTime(y, 9, 30), $"Q2 FY {y}-{(y + 1) % 100:D2} (Jul-Sep)"),
            "Q3" => (new DateTime(y, 10, 1), new DateTime(y, 12, 31), $"Q3 FY {y}-{(y + 1) % 100:D2} (Oct-Dec)"),
            "Q4" => (new DateTime(y + 1, 1, 1), new DateTime(y + 1, 3, 31), $"Q4 FY {y}-{(y + 1) % 100:D2} (Jan-Mar)"),
            "H1" => (new DateTime(y, 4, 1), new DateTime(y, 9, 30), $"H1 FY {y}-{(y + 1) % 100:D2} (Apr-Sep)"),
            "H2" => (new DateTime(y, 10, 1), new DateTime(y + 1, 3, 31), $"H2 FY {y}-{(y + 1) % 100:D2} (Oct-Mar)"),
            _ => (new DateTime(y, 4, 1), new DateTime(y + 1, 3, 31), $"FY {y}-{(y + 1) % 100:D2} (Full Year)")
        };
    }
}
