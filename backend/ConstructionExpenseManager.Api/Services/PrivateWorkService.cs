using ConstructionExpenseManager.Api.DTOs.PrivateWorks;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Repositories;

namespace ConstructionExpenseManager.Api.Services;

public class PrivateWorkService : IPrivateWorkService
{
    private readonly IPrivateWorkRepository _repository;

    public PrivateWorkService(IPrivateWorkRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PrivateWorkDto>> GetAllAsync()
    {
        var works = await _repository.GetAllWithDetailsAsync();
        return works.Select(ToDto).ToList();
    }

    public async Task<PrivateWorkDto?> GetByIdAsync(int id)
    {
        var work = await _repository.GetWithDetailsAsync(id);
        return work is null ? null : ToDto(work);
    }

    public async Task<PrivateWorkDto> CreateAsync(SavePrivateWorkDto dto, int createdByUserId)
    {
        var work = new PrivateWork { CreatedByUserId = createdByUserId };
        Apply(work, dto);

        await _repository.AddAsync(work);
        await _repository.SaveChangesAsync();

        return ToDto(work);
    }

    public async Task<PrivateWorkDto?> UpdateAsync(int id, SavePrivateWorkDto dto)
    {
        var work = await _repository.GetWithDetailsAsync(id);
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

    // ----- Milestones -----

    public async Task<PrivateWorkMilestoneDto?> AddMilestoneAsync(int privateWorkId, SavePrivateWorkMilestoneDto dto)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        if (work is null) return null;

        var milestone = new PrivateWorkMilestone { PrivateWorkId = privateWorkId };
        Apply(milestone, dto);
        work.Milestones.Add(milestone);

        await _repository.SaveChangesAsync();
        return ToDto(milestone, work);
    }

    public async Task<PrivateWorkMilestoneDto?> UpdateMilestoneAsync(int privateWorkId, int milestoneId, SavePrivateWorkMilestoneDto dto)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        var milestone = work?.Milestones.FirstOrDefault(m => m.Id == milestoneId);
        if (milestone is null) return null;

        Apply(milestone, dto);
        await _repository.SaveChangesAsync();
        return ToDto(milestone, work!);
    }

    public async Task<bool> DeleteMilestoneAsync(int privateWorkId, int milestoneId)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        var milestone = work?.Milestones.FirstOrDefault(m => m.Id == milestoneId);
        if (milestone is null) return false;

        work!.Milestones.Remove(milestone);
        await _repository.SaveChangesAsync();
        return true;
    }

    // ----- Categories -----

    public async Task<PrivateWorkCategoryDto?> AddCategoryAsync(int privateWorkId, SavePrivateWorkCategoryDto dto)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        if (work is null) return null;

        var category = new PrivateWorkCategory { PrivateWorkId = privateWorkId };
        Apply(category, dto);
        work.Categories.Add(category);

        await _repository.SaveChangesAsync();
        return ToDto(category);
    }

    public async Task<PrivateWorkCategoryDto?> UpdateCategoryAsync(int privateWorkId, int categoryId, SavePrivateWorkCategoryDto dto)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        var category = work?.Categories.FirstOrDefault(c => c.Id == categoryId);
        if (category is null) return null;

        Apply(category, dto);
        await _repository.SaveChangesAsync();
        return ToDto(category);
    }

    public async Task<bool> DeleteCategoryAsync(int privateWorkId, int categoryId)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        var category = work?.Categories.FirstOrDefault(c => c.Id == categoryId);
        if (category is null) return false;

        work!.Categories.Remove(category);
        await _repository.SaveChangesAsync();
        return true;
    }

    // ----- Category Payments -----

    public async Task<PrivateWorkCategoryPaymentDto?> AddCategoryPaymentAsync(int privateWorkId, int categoryId, SavePrivateWorkCategoryPaymentDto dto)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        var category = work?.Categories.FirstOrDefault(c => c.Id == categoryId);
        if (category is null) return null;

        var payment = new PrivateWorkCategoryPayment { CategoryId = categoryId };
        Apply(payment, dto);
        category.Payments.Add(payment);

        await _repository.SaveChangesAsync();
        return ToDto(payment);
    }

    public async Task<PrivateWorkCategoryPaymentDto?> UpdateCategoryPaymentAsync(int privateWorkId, int categoryId, int paymentId, SavePrivateWorkCategoryPaymentDto dto)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        var category = work?.Categories.FirstOrDefault(c => c.Id == categoryId);
        var payment = category?.Payments.FirstOrDefault(p => p.Id == paymentId);
        if (payment is null) return null;

        Apply(payment, dto);
        await _repository.SaveChangesAsync();
        return ToDto(payment);
    }

    public async Task<bool> DeleteCategoryPaymentAsync(int privateWorkId, int categoryId, int paymentId)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        var category = work?.Categories.FirstOrDefault(c => c.Id == categoryId);
        var payment = category?.Payments.FirstOrDefault(p => p.Id == paymentId);
        if (payment is null) return false;

        category!.Payments.Remove(payment);
        await _repository.SaveChangesAsync();
        return true;
    }

    // ----- Materials -----

    public async Task<PrivateWorkMaterialDto?> AddMaterialAsync(int privateWorkId, SavePrivateWorkMaterialDto dto)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        if (work is null) return null;

        var material = new PrivateWorkMaterial { PrivateWorkId = privateWorkId };
        Apply(material, dto);
        work.Materials.Add(material);

        await _repository.SaveChangesAsync();
        return ToDto(material);
    }

    public async Task<PrivateWorkMaterialDto?> UpdateMaterialAsync(int privateWorkId, int materialId, SavePrivateWorkMaterialDto dto)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        var material = work?.Materials.FirstOrDefault(m => m.Id == materialId);
        if (material is null) return null;

        Apply(material, dto);
        await _repository.SaveChangesAsync();
        return ToDto(material);
    }

    public async Task<bool> DeleteMaterialAsync(int privateWorkId, int materialId)
    {
        var work = await _repository.GetWithDetailsAsync(privateWorkId);
        var material = work?.Materials.FirstOrDefault(m => m.Id == materialId);
        if (material is null) return false;

        work!.Materials.Remove(material);
        await _repository.SaveChangesAsync();
        return true;
    }

    // ----- Apply helpers -----

    private static void Apply(PrivateWork work, SavePrivateWorkDto dto)
    {
        work.ClientName = dto.ClientName.Trim();
        work.WorkDescription = dto.WorkDescription.Trim();
        work.AreaSqft = dto.AreaSqft;
        work.RatePerSqft = dto.RatePerSqft;
    }

    private static void Apply(PrivateWorkMilestone milestone, SavePrivateWorkMilestoneDto dto)
    {
        milestone.StageName = dto.StageName.Trim();
        milestone.PercentOfTotal = dto.PercentOfTotal;
        milestone.PaidAmount = dto.PaidAmount;
        milestone.PaidDate = dto.PaidDate;
        milestone.Status = Enum.TryParse<MilestoneStatus>(dto.Status, true, out var status) ? status : MilestoneStatus.Pending;
        milestone.SortOrder = dto.SortOrder;
    }

    private static void Apply(PrivateWorkCategory category, SavePrivateWorkCategoryDto dto)
    {
        category.CategoryName = dto.CategoryName.Trim();
        category.WorkerName = dto.WorkerName.Trim();
        category.RateBasis = dto.RateBasis.Trim();
        category.AgreedTotalAmount = dto.AgreedTotalAmount;
    }

    private static void Apply(PrivateWorkCategoryPayment payment, SavePrivateWorkCategoryPaymentDto dto)
    {
        payment.PaymentDate = dto.PaymentDate;
        payment.Amount = dto.Amount;
        payment.Remarks = dto.Remarks?.Trim();
    }

    private static void Apply(PrivateWorkMaterial material, SavePrivateWorkMaterialDto dto)
    {
        material.MaterialName = dto.MaterialName.Trim();
        material.VendorName = dto.VendorName.Trim();
        material.Amount = dto.Amount;
        material.PaymentDate = dto.PaymentDate;
    }

    // ----- DTO mapping -----

    private static PrivateWorkMilestoneDto ToDto(PrivateWorkMilestone m, PrivateWork work) => new()
    {
        Id = m.Id,
        PrivateWorkId = m.PrivateWorkId,
        StageName = m.StageName,
        PercentOfTotal = m.PercentOfTotal,
        Amount = Math.Round(work.AreaSqft * work.RatePerSqft * m.PercentOfTotal / 100, 2),
        PaidAmount = m.PaidAmount,
        PaidDate = m.PaidDate,
        Status = m.Status.ToString(),
        SortOrder = m.SortOrder
    };

    private static PrivateWorkCategoryPaymentDto ToDto(PrivateWorkCategoryPayment p) => new()
    {
        Id = p.Id,
        CategoryId = p.CategoryId,
        PaymentDate = p.PaymentDate,
        Amount = p.Amount,
        Remarks = p.Remarks
    };

    private static PrivateWorkCategoryDto ToDto(PrivateWorkCategory c)
    {
        var totalPaid = c.Payments.Sum(p => p.Amount);
        return new PrivateWorkCategoryDto
        {
            Id = c.Id,
            PrivateWorkId = c.PrivateWorkId,
            CategoryName = c.CategoryName,
            WorkerName = c.WorkerName,
            RateBasis = c.RateBasis,
            AgreedTotalAmount = c.AgreedTotalAmount,
            TotalPaid = totalPaid,
            RemainingAmount = c.AgreedTotalAmount - totalPaid,
            Payments = c.Payments.OrderByDescending(p => p.PaymentDate).Select(ToDto).ToList()
        };
    }

    private static PrivateWorkMaterialDto ToDto(PrivateWorkMaterial m) => new()
    {
        Id = m.Id,
        PrivateWorkId = m.PrivateWorkId,
        MaterialName = m.MaterialName,
        VendorName = m.VendorName,
        Amount = m.Amount,
        PaymentDate = m.PaymentDate
    };

    public static PrivateWorkDto ToDto(PrivateWork work)
    {
        var totalAmount = work.AreaSqft * work.RatePerSqft;
        var milestoneDtos = work.Milestones.OrderBy(m => m.SortOrder).Select(m => ToDto(m, work)).ToList();
        var categoryDtos = work.Categories.Select(ToDto).ToList();
        var materialDtos = work.Materials.OrderByDescending(m => m.PaymentDate).Select(ToDto).ToList();

        // Money-flow summary:
        // Total Received = sum of milestone payments actually received so far.
        // Pending to Receive = Total Contract Amount - Total Received (not the sum of per-milestone gaps,
        // so it stays correct even if milestone percentages don't add up to exactly 100%).
        // Total Used = worker/vendor payments + material payments made so far.
        // In-Hand = Total Received - Total Used.
        var totalWorkerPaid = categoryDtos.Sum(c => c.TotalPaid);
        var totalMaterialAmount = materialDtos.Sum(m => m.Amount);
        var totalReceived = milestoneDtos.Sum(m => m.PaidAmount);
        var totalUsed = totalWorkerPaid + totalMaterialAmount;

        return new PrivateWorkDto
        {
            Id = work.Id,
            ClientName = work.ClientName,
            WorkDescription = work.WorkDescription,
            AreaSqft = work.AreaSqft,
            RatePerSqft = work.RatePerSqft,
            TotalAmount = totalAmount,
            TotalMilestonePaid = totalReceived,
            TotalMilestonePending = milestoneDtos.Sum(m => m.Amount - m.PaidAmount),
            TotalWorkerPaid = totalWorkerPaid,
            TotalWorkerRemaining = categoryDtos.Sum(c => c.RemainingAmount),
            TotalMaterialAmount = totalMaterialAmount,
            TotalReceived = totalReceived,
            PendingToReceive = totalAmount - totalReceived,
            TotalUsed = totalUsed,
            InHandAmount = totalReceived - totalUsed,
            Milestones = milestoneDtos,
            Categories = categoryDtos,
            Materials = materialDtos,
            CreatedAt = work.CreatedAt,
            UpdatedAt = work.UpdatedAt
        };
    }
}
