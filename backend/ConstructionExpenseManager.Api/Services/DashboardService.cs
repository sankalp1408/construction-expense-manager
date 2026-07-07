using ConstructionExpenseManager.Api.DTOs.Dashboard;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Repositories;

namespace ConstructionExpenseManager.Api.Services;

public class DashboardService : IDashboardService
{
    private readonly ITenderWorkRepository _tenderWorkRepository;
    private readonly ICommissionWorkRepository _commissionWorkRepository;
    private readonly IPrivateWorkRepository _privateWorkRepository;
    private readonly IGstVendorEntryRepository _gstVendorEntryRepository;

    public DashboardService(
        ITenderWorkRepository tenderWorkRepository,
        ICommissionWorkRepository commissionWorkRepository,
        IPrivateWorkRepository privateWorkRepository,
        IGstVendorEntryRepository gstVendorEntryRepository)
    {
        _tenderWorkRepository = tenderWorkRepository;
        _commissionWorkRepository = commissionWorkRepository;
        _privateWorkRepository = privateWorkRepository;
        _gstVendorEntryRepository = gstVendorEntryRepository;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync(DashboardQueryDto query)
    {
        var from = query.FromDate ?? DateTime.MinValue;
        var to = query.ToDate ?? DateTime.MaxValue;

        var summary = new DashboardSummaryDto();

        var tenderWorks = await _tenderWorkRepository.FindAsync(t => t.CreatedAt >= from && t.CreatedAt <= to);
        var tenderDtos = tenderWorks.Select(TenderWorkService.ToDto).ToList();

        var commissionWorks = await _commissionWorkRepository.FindAsync(c => c.CreatedAt >= from && c.CreatedAt <= to);
        var commissionDtos = commissionWorks.Select(CommissionWorkService.ToDto).ToList();

        var privateWorksInRange = await _privateWorkRepository.FindAsync(p => p.CreatedAt >= from && p.CreatedAt <= to);
        var privateWorkIds = privateWorksInRange.Select(p => p.Id).ToHashSet();
        var allPrivateWithDetails = await _privateWorkRepository.GetAllWithDetailsAsync();
        var privateDtos = allPrivateWithDetails.Where(p => privateWorkIds.Contains(p.Id)).Select(PrivateWorkService.ToDto).ToList();

        var tenderWorkIds = tenderDtos.Select(t => t.Id).ToHashSet();
        var commissionWorkIds = commissionDtos.Select(c => c.Id).ToHashSet();

        var tenderVendorCommission = (await _gstVendorEntryRepository.FindWithRepaymentsAsync(
                g => g.WorkType == GstWorkType.Tender && tenderWorkIds.Contains(g.WorkId)))
            .Select(GstVendorLedgerService.ToDto).Sum(d => d.CommissionAmount);

        var commissionVendorCommission = (await _gstVendorEntryRepository.FindWithRepaymentsAsync(
                g => g.WorkType == GstWorkType.Commission && commissionWorkIds.Contains(g.WorkId)))
            .Select(GstVendorLedgerService.ToDto).Sum(d => d.CommissionAmount);

        // ----- Original 6 top tiles -----

        summary.TenderWorkCount = tenderDtos.Count;
        summary.TotalTenderProfit = tenderDtos.Sum(t => t.Profit);
        summary.TotalTenderBilledAmount = tenderDtos.Sum(t => t.BilledAmount);

        summary.CommissionWorkCount = commissionDtos.Count;
        summary.TotalCommissionEarned = commissionDtos.Sum(c => c.CommissionAmount);

        summary.PrivateWorkCount = privateDtos.Count;
        summary.TotalPrivateWorkAmount = privateDtos.Sum(p => p.TotalAmount);
        summary.TotalPrivatePendingPayments = privateDtos.Sum(p => p.TotalMilestonePending + p.TotalWorkerRemaining);

        summary.TotalWorkCount = summary.TenderWorkCount + summary.CommissionWorkCount + summary.PrivateWorkCount;

        // ----- Section 1: My Own Work (Tender) Summary -----

        var tenderSummary = new TenderSummaryDto
        {
            TotalTenderAmount = tenderDtos.Sum(t => t.TenderAmount),
            TotalBillReceived = tenderDtos.Sum(t => t.BilledAmount),
            TotalExpense = tenderDtos.Sum(t => t.WorkExpenditure),
            TotalGst = tenderDtos.Sum(t => t.GstTotal),
            TotalBilledGst = tenderDtos.Sum(t => t.BilledGst),
            ExtraGstTaken = tenderDtos.Sum(t => t.ExtraGstBill)
        };
        tenderSummary.GstFiling = tenderSummary.TotalGst - tenderSummary.TotalBilledGst - tenderSummary.ExtraGstTaken;
        tenderSummary.TotalCommissionForGst = tenderDtos.Sum(t => t.GstBillCommission) + tenderVendorCommission;
        tenderSummary.Profit = tenderSummary.TotalBillReceived - tenderSummary.TotalExpense
            - tenderSummary.GstFiling - tenderSummary.TotalCommissionForGst;
        summary.TenderSummary = tenderSummary;

        // ----- Section 2: Commission Work Summary -----

        var commissionSummary = new CommissionSummaryDto
        {
            TotalTenderAmount = commissionDtos.Sum(c => c.TenderWorkAmount),
            TotalBillReceived = commissionDtos.Sum(c => c.TenderWorkAmount),
            CommissionAmount = commissionDtos.Sum(c => c.CommissionAmount),
            TotalGst = commissionDtos.Sum(c => c.GstAmount),
            TotalBilledGst = commissionDtos.Sum(c => c.BilledGst),
            ExtraGstTaken = commissionDtos.Sum(c => c.ExtraGstBill)
        };
        commissionSummary.GstFiling = commissionSummary.TotalGst - commissionSummary.TotalBilledGst - commissionSummary.ExtraGstTaken;
        commissionSummary.TotalCommissionForGst = commissionDtos.Sum(c => c.GstBillCommission) + commissionVendorCommission;
        commissionSummary.Profit = commissionSummary.CommissionAmount + commissionSummary.ExtraGstTaken
            - commissionSummary.TotalCommissionForGst;
        summary.CommissionSummary = commissionSummary;

        // ----- Section 3: Private Work Summary -----

        var privateSummary = new PrivateWorkSummaryDto
        {
            TotalWorkAmount = privateDtos.Sum(p => p.TotalAmount),
            PaymentReceived = privateDtos.Sum(p => p.TotalReceived),
            TotalExpense = privateDtos.Sum(p => p.TotalUsed)
        };
        privateSummary.Profit = privateSummary.PaymentReceived - privateSummary.TotalExpense;
        summary.PrivateWorkSummary = privateSummary;

        // ----- Section 4: Overall Summary -----

        var overallSummary = new OverallSummaryDto
        {
            TotalPayment = tenderSummary.TotalTenderAmount + commissionSummary.TotalTenderAmount + privateSummary.TotalWorkAmount,
            TotalPaymentReceived = tenderSummary.TotalBillReceived + commissionSummary.TotalBillReceived + privateSummary.PaymentReceived,
            TotalGst = tenderSummary.TotalGst + commissionSummary.TotalGst,
            TotalExtraGstTaken = tenderSummary.ExtraGstTaken + commissionSummary.ExtraGstTaken,
            TotalGstCommission = tenderSummary.TotalCommissionForGst + commissionSummary.TotalCommissionForGst,
            TotalGstFiling = tenderSummary.GstFiling + commissionSummary.GstFiling,
            TotalProfit = tenderSummary.Profit + commissionSummary.Profit + privateSummary.Profit
        };
        overallSummary.TotalPaymentBalance = overallSummary.TotalPayment - overallSummary.TotalPaymentReceived;
        summary.OverallSummary = overallSummary;

        return summary;
    }
}
