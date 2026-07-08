using ConstructionExpenseManager.Api.DTOs.PrivateWorks;

namespace ConstructionExpenseManager.Api.Services;

public interface IPrivateWorkService
{
    Task<List<PrivateWorkDto>> GetAllAsync();
    Task<PrivateWorkDto?> GetByIdAsync(int id);
    Task<PrivateWorkDto> CreateAsync(SavePrivateWorkDto dto, int createdByUserId);
    Task<PrivateWorkDto?> UpdateAsync(int id, SavePrivateWorkDto dto);
    Task<bool> DeleteAsync(int id);

    Task<PrivateWorkMilestoneDto?> AddMilestoneAsync(int privateWorkId, SavePrivateWorkMilestoneDto dto);
    Task<PrivateWorkMilestoneDto?> UpdateMilestoneAsync(int privateWorkId, int milestoneId, SavePrivateWorkMilestoneDto dto);
    Task<bool> DeleteMilestoneAsync(int privateWorkId, int milestoneId);

    Task<PrivateWorkCategoryDto?> AddCategoryAsync(int privateWorkId, SavePrivateWorkCategoryDto dto);
    Task<PrivateWorkCategoryDto?> UpdateCategoryAsync(int privateWorkId, int categoryId, SavePrivateWorkCategoryDto dto);
    Task<bool> DeleteCategoryAsync(int privateWorkId, int categoryId);

    Task<PrivateWorkCategoryPaymentDto?> AddCategoryPaymentAsync(int privateWorkId, int categoryId, SavePrivateWorkCategoryPaymentDto dto);
    Task<PrivateWorkCategoryPaymentDto?> UpdateCategoryPaymentAsync(int privateWorkId, int categoryId, int paymentId, SavePrivateWorkCategoryPaymentDto dto);
    Task<bool> DeleteCategoryPaymentAsync(int privateWorkId, int categoryId, int paymentId);

    Task<PrivateWorkMaterialDto?> AddMaterialAsync(int privateWorkId, SavePrivateWorkMaterialDto dto);
    Task<PrivateWorkMaterialDto?> UpdateMaterialAsync(int privateWorkId, int materialId, SavePrivateWorkMaterialDto dto);
    Task<bool> DeleteMaterialAsync(int privateWorkId, int materialId);

    Task<PrivateWorkDepartmentalLabourDto?> AddDepartmentalLabourAsync(int privateWorkId, SavePrivateWorkDepartmentalLabourDto dto);
    Task<PrivateWorkDepartmentalLabourDto?> UpdateDepartmentalLabourAsync(int privateWorkId, int departmentalLabourId, SavePrivateWorkDepartmentalLabourDto dto);
    Task<bool> DeleteDepartmentalLabourAsync(int privateWorkId, int departmentalLabourId);
}
