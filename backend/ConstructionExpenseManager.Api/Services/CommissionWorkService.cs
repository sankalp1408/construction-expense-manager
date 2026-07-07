using ConstructionExpenseManager.Api.DTOs.CommissionWorks;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Repositories;

namespace ConstructionExpenseManager.Api.Services;

public class CommissionWorkService : ICommissionWorkService
{
    private readonly ICommissionWorkRepository _repository;

    public CommissionWorkService(ICommissionWorkRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CommissionWorkDto>> GetAllAsync()
    {
        var works = await _repository.GetAllAsync();
        return works.OrderByDescending(w => w.Id).Select(ToDto).ToList();
    }

    public async Task<CommissionWorkDto?> GetByIdAsync(int id)
    {
        var work = await _repository.GetByIdAsync(id);
        return work is null ? null : ToDto(work);
    }

    public async Task<CommissionWorkDto> CreateAsync(SaveCommissionWorkDto dto, int createdByUserId)
    {
        var work = new CommissionWork { CreatedByUserId = createdByUserId };
        Apply(work, dto);

        await _repository.AddAsync(work);
        await _repository.SaveChangesAsync();

        return ToDto(work);
    }

    public async Task<CommissionWorkDto?> UpdateAsync(int id, SaveCommissionWorkDto dto)
    {
        var work = await _repository.GetByIdAsync(id);
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

    private static void Apply(CommissionWork work, SaveCommissionWorkDto dto)
    {
        work.WorkName = dto.WorkName.Trim();
        work.PartyName = dto.PartyName.Trim();
        work.TenderWorkAmount = dto.TenderWorkAmount;
        work.CommissionPercent = dto.CommissionPercent;
        work.GstAmount = dto.GstAmount;
        work.BilledGst = dto.BilledGst;
        work.ExtraGstBill = dto.ExtraGstBill;
        work.GstBillCommission = dto.GstBillCommission;
    }

    public static CommissionWorkDto ToDto(CommissionWork work) => new()
    {
        Id = work.Id,
        WorkName = work.WorkName,
        PartyName = work.PartyName,
        TenderWorkAmount = work.TenderWorkAmount,
        CommissionPercent = work.CommissionPercent,
        GstAmount = work.GstAmount,
        BilledGst = work.BilledGst,
        ExtraGstBill = work.ExtraGstBill,
        GstBillCommission = work.GstBillCommission,
        CommissionAmount = Math.Round(work.TenderWorkAmount * work.CommissionPercent / 100, 2),
        GstFiling = work.GstAmount - (work.BilledGst + work.ExtraGstBill),
        CreatedAt = work.CreatedAt,
        UpdatedAt = work.UpdatedAt
    };
}
