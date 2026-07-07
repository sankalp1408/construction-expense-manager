using ConstructionExpenseManager.Api.DTOs.Common;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Repositories;

namespace ConstructionExpenseManager.Api.Services;

public class GstVendorLedgerService : IGstVendorLedgerService
{
    private readonly IGstVendorEntryRepository _repository;

    public GstVendorLedgerService(IGstVendorEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<GstVendorLedgerDto> GetLedgerAsync(GstWorkType workType, int workId)
    {
        var entries = await _repository.GetForWorkAsync(workType, workId);
        var dtos = entries.Select(ToDto).ToList();

        return new GstVendorLedgerDto
        {
            Entries = dtos,
            TotalGstBilled = dtos.Sum(d => d.GstBillAmount),
            TotalCommissionAmount = dtos.Sum(d => d.CommissionAmount),
            TotalNetPayable = dtos.Sum(d => d.NetPayable),
            TotalReceivedBack = dtos.Sum(d => d.TotalReceivedSoFar),
            TotalPendingFromVendors = dtos.Sum(d => d.PendingAmount)
        };
    }

    public async Task<GstVendorEntryDto> AddEntryAsync(GstWorkType workType, int workId, SaveGstVendorEntryDto dto)
    {
        var entry = new GstVendorEntry
        {
            WorkType = workType,
            WorkId = workId,
            VendorName = dto.VendorName.Trim(),
            GstBillAmount = dto.GstBillAmount,
            CommissionPercent = dto.CommissionPercent,
            SentDate = dto.SentDate
        };

        await _repository.AddAsync(entry);
        await _repository.SaveChangesAsync();

        return ToDto(entry);
    }

    public async Task<GstVendorEntryDto?> UpdateEntryAsync(GstWorkType workType, int workId, int entryId, SaveGstVendorEntryDto dto)
    {
        var entry = await _repository.GetByIdWithRepaymentsAsync(entryId);
        if (entry is null || entry.WorkType != workType || entry.WorkId != workId)
        {
            return null;
        }

        entry.VendorName = dto.VendorName.Trim();
        entry.GstBillAmount = dto.GstBillAmount;
        entry.CommissionPercent = dto.CommissionPercent;
        entry.SentDate = dto.SentDate;

        _repository.Update(entry);
        await _repository.SaveChangesAsync();

        return ToDto(entry);
    }

    public async Task<bool> DeleteEntryAsync(GstWorkType workType, int workId, int entryId)
    {
        var entry = await _repository.GetByIdAsync(entryId);
        if (entry is null || entry.WorkType != workType || entry.WorkId != workId)
        {
            return false;
        }

        _repository.Remove(entry);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<GstVendorRepaymentDto?> AddRepaymentAsync(GstWorkType workType, int workId, int entryId, SaveGstVendorRepaymentDto dto)
    {
        var entry = await _repository.GetByIdWithRepaymentsAsync(entryId);
        if (entry is null || entry.WorkType != workType || entry.WorkId != workId)
        {
            return null;
        }

        var repayment = new GstVendorRepayment { GstVendorEntryId = entryId };
        Apply(repayment, dto);
        entry.Repayments.Add(repayment);

        await _repository.SaveChangesAsync();
        return ToDto(repayment);
    }

    public async Task<GstVendorRepaymentDto?> UpdateRepaymentAsync(GstWorkType workType, int workId, int entryId, int repaymentId, SaveGstVendorRepaymentDto dto)
    {
        var entry = await _repository.GetByIdWithRepaymentsAsync(entryId);
        var repayment = entry?.Repayments.FirstOrDefault(r => r.Id == repaymentId);
        if (entry is null || entry.WorkType != workType || entry.WorkId != workId || repayment is null)
        {
            return null;
        }

        Apply(repayment, dto);
        await _repository.SaveChangesAsync();
        return ToDto(repayment);
    }

    public async Task<bool> DeleteRepaymentAsync(GstWorkType workType, int workId, int entryId, int repaymentId)
    {
        var entry = await _repository.GetByIdWithRepaymentsAsync(entryId);
        var repayment = entry?.Repayments.FirstOrDefault(r => r.Id == repaymentId);
        if (entry is null || entry.WorkType != workType || entry.WorkId != workId || repayment is null)
        {
            return false;
        }

        entry.Repayments.Remove(repayment);
        await _repository.SaveChangesAsync();
        return true;
    }

    private static void Apply(GstVendorRepayment repayment, SaveGstVendorRepaymentDto dto)
    {
        repayment.ReceivedDate = dto.ReceivedDate;
        repayment.AmountReceived = dto.AmountReceived;
        repayment.Mode = Enum.TryParse<GstVendorRepaymentMode>(dto.Mode, true, out var mode) ? mode : GstVendorRepaymentMode.Cash;
    }

    private static GstVendorRepaymentDto ToDto(GstVendorRepayment repayment) => new()
    {
        Id = repayment.Id,
        GstVendorEntryId = repayment.GstVendorEntryId,
        ReceivedDate = repayment.ReceivedDate,
        AmountReceived = repayment.AmountReceived,
        Mode = repayment.Mode.ToString()
    };

    public static GstVendorEntryDto ToDto(GstVendorEntry entry)
    {
        var commissionAmount = Math.Round(entry.GstBillAmount * entry.CommissionPercent / 100, 2);
        var netPayable = entry.GstBillAmount - commissionAmount;
        var totalReceivedSoFar = entry.Repayments.Sum(r => r.AmountReceived);

        return new GstVendorEntryDto
        {
            Id = entry.Id,
            WorkId = entry.WorkId,
            VendorName = entry.VendorName,
            GstBillAmount = entry.GstBillAmount,
            SentDate = entry.SentDate,
            CommissionPercent = entry.CommissionPercent,
            CommissionAmount = commissionAmount,
            NetPayable = netPayable,
            TotalReceivedSoFar = totalReceivedSoFar,
            PendingAmount = netPayable - totalReceivedSoFar,
            Repayments = entry.Repayments.OrderByDescending(r => r.ReceivedDate).Select(ToDto).ToList(),
            CreatedAt = entry.CreatedAt
        };
    }
}
