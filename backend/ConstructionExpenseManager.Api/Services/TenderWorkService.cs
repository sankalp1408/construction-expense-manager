using ConstructionExpenseManager.Api.DTOs.TenderWorks;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Repositories;

namespace ConstructionExpenseManager.Api.Services;

public class TenderWorkService : ITenderWorkService
{
    private readonly ITenderWorkRepository _repository;

    public TenderWorkService(ITenderWorkRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TenderWorkDto>> GetAllAsync()
    {
        var works = await _repository.GetAllWithRaBillsAsync();
        return works.Select(ToDto).ToList();
    }

    public async Task<TenderWorkDto?> GetByIdAsync(int id)
    {
        var work = await _repository.GetWithRaBillsAsync(id);
        return work is null ? null : ToDto(work);
    }

    public async Task<TenderWorkDto> CreateAsync(SaveTenderWorkDto dto, int createdByUserId)
    {
        var work = new TenderWork { CreatedByUserId = createdByUserId };
        Apply(work, dto);

        await _repository.AddAsync(work);
        await _repository.SaveChangesAsync();

        return ToDto(work);
    }

    public async Task<TenderWorkDto?> UpdateAsync(int id, SaveTenderWorkDto dto)
    {
        var work = await _repository.GetWithRaBillsAsync(id);
        if (work is null)
        {
            return null;
        }

        Apply(work, dto);
        work.UpdatedAt = DateTime.UtcNow;

        _repository.Update(work);
        await _repository.SaveChangesAsync();

        return ToDto(work);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var work = await _repository.GetByIdAsync(id);
        if (work is null)
        {
            return false;
        }

        _repository.Remove(work);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<List<TenderRaBillDto>> GetRaBillsAsync(int tenderWorkId)
    {
        var work = await _repository.GetWithRaBillsAsync(tenderWorkId);
        return work is null ? new List<TenderRaBillDto>() : work.RaBills.Select(ToDto).OrderBy(b => b.BillDate).ToList();
    }

    public async Task<TenderRaBillDto?> AddRaBillAsync(int tenderWorkId, SaveTenderRaBillDto dto)
    {
        var work = await _repository.GetWithRaBillsAsync(tenderWorkId);
        if (work is null)
        {
            return null;
        }

        var bill = new TenderRaBill { TenderWorkId = tenderWorkId };
        Apply(bill, dto);
        work.RaBills.Add(bill);

        await _repository.SaveChangesAsync();
        return ToDto(bill);
    }

    public async Task<TenderRaBillDto?> UpdateRaBillAsync(int tenderWorkId, int raBillId, SaveTenderRaBillDto dto)
    {
        var work = await _repository.GetWithRaBillsAsync(tenderWorkId);
        var bill = work?.RaBills.FirstOrDefault(b => b.Id == raBillId);
        if (bill is null)
        {
            return null;
        }

        Apply(bill, dto);
        await _repository.SaveChangesAsync();
        return ToDto(bill);
    }

    public async Task<bool> DeleteRaBillAsync(int tenderWorkId, int raBillId)
    {
        var work = await _repository.GetWithRaBillsAsync(tenderWorkId);
        var bill = work?.RaBills.FirstOrDefault(b => b.Id == raBillId);
        if (bill is null)
        {
            return false;
        }

        work!.RaBills.Remove(bill);
        await _repository.SaveChangesAsync();
        return true;
    }

    private static void Apply(TenderWork work, SaveTenderWorkDto dto)
    {
        work.TenderName = dto.TenderName.Trim();
        work.NameOfWork = dto.NameOfWork.Trim();
        work.TenderAmount = dto.TenderAmount;
        work.TenderFee = dto.TenderFee;
        work.TenderEMD = dto.TenderEMD;
        work.TenderFilingAmount = dto.TenderFilingAmount;
        work.GstTotal = dto.GstTotal;
        work.BilledGst = dto.BilledGst;
        work.ExtraGstBill = dto.ExtraGstBill;
        work.WorkExpenditure = dto.WorkExpenditure;
        work.SecurityDepositPercent = dto.SecurityDepositPercent;
        work.OfficeProtocolPercent = dto.OfficeProtocolPercent;
        work.CorporatorName = dto.CorporatorName.Trim();
        work.CorporatorProtocolPercent = dto.CorporatorProtocolPercent;
        work.GstBillCommission = dto.GstBillCommission;
    }

    private static void Apply(TenderRaBill bill, SaveTenderRaBillDto dto)
    {
        bill.RaBillNumber = dto.RaBillNumber.Trim();
        bill.BillDate = dto.BillDate;
        bill.BilledAmount = dto.BilledAmount;
        bill.CorporatorCommissionPercent = dto.CorporatorCommissionPercent;
        bill.OfficerCommissionPercent = dto.OfficerCommissionPercent;
        bill.Remarks = dto.Remarks?.Trim();
    }

    private static TenderRaBillDto ToDto(TenderRaBill bill) => new()
    {
        Id = bill.Id,
        TenderWorkId = bill.TenderWorkId,
        RaBillNumber = bill.RaBillNumber,
        BillDate = bill.BillDate,
        BilledAmount = bill.BilledAmount,
        CorporatorCommissionPercent = bill.CorporatorCommissionPercent,
        CorporatorCommissionAmount = Math.Round(bill.BilledAmount * bill.CorporatorCommissionPercent / 100, 2),
        OfficerCommissionPercent = bill.OfficerCommissionPercent,
        OfficerCommissionAmount = Math.Round(bill.BilledAmount * bill.OfficerCommissionPercent / 100, 2),
        Remarks = bill.Remarks,
        CreatedAt = bill.CreatedAt
    };

    public static TenderWorkDto ToDto(TenderWork work)
    {
        // Billed Amount = sum of all RA Bill "Billed Amount" entries for this tender.
        // Recalculates automatically whenever an RA bill is added/edited/deleted.
        var billedAmount = work.RaBills.Sum(b => b.BilledAmount);

        // GST Filing = GST (Total) - (Billed GST + Extra GST Bill).
        var gstFiling = work.GstTotal - (work.BilledGst + work.ExtraGstBill);

        // Security Deposit / Office Protocol / Corporator Protocol are % of Billed Amount.
        // Corporator Name is text-only and must never enter a numeric calculation.
        var securityDepositAmount = Math.Round(billedAmount * work.SecurityDepositPercent / 100, 2);
        var officeProtocolAmount = Math.Round(billedAmount * work.OfficeProtocolPercent / 100, 2);
        var corporatorProtocolAmount = Math.Round(billedAmount * work.CorporatorProtocolPercent / 100, 2);

        var profit = billedAmount - (work.TenderFee + work.TenderEMD + work.TenderFilingAmount + work.WorkExpenditure
            + securityDepositAmount + officeProtocolAmount + corporatorProtocolAmount
            + work.GstBillCommission + gstFiling);

        return new TenderWorkDto
        {
            Id = work.Id,
            TenderName = work.TenderName,
            NameOfWork = work.NameOfWork,
            TenderAmount = work.TenderAmount,
            TenderFee = work.TenderFee,
            TenderEMD = work.TenderEMD,
            TenderFilingAmount = work.TenderFilingAmount,
            GstTotal = work.GstTotal,
            BilledGst = work.BilledGst,
            ExtraGstBill = work.ExtraGstBill,
            WorkExpenditure = work.WorkExpenditure,
            SecurityDepositPercent = work.SecurityDepositPercent,
            OfficeProtocolPercent = work.OfficeProtocolPercent,
            CorporatorName = work.CorporatorName,
            CorporatorProtocolPercent = work.CorporatorProtocolPercent,
            GstBillCommission = work.GstBillCommission,
            BilledAmount = billedAmount,
            GstFiling = gstFiling,
            SecurityDepositAmount = securityDepositAmount,
            OfficeProtocolAmount = officeProtocolAmount,
            CorporatorProtocolAmount = corporatorProtocolAmount,
            Profit = profit,
            RaBillCount = work.RaBills.Count,
            TotalRaBilledAmount = work.RaBills.Sum(b => b.BilledAmount),
            CreatedAt = work.CreatedAt,
            UpdatedAt = work.UpdatedAt
        };
    }
}
