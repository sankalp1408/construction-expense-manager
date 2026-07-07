using ConstructionExpenseManager.Api.DTOs.CommissionWorks;

namespace ConstructionExpenseManager.Api.Services;

public interface ICommissionWorkService
{
    Task<List<CommissionWorkDto>> GetAllAsync();
    Task<CommissionWorkDto?> GetByIdAsync(int id);
    Task<CommissionWorkDto> CreateAsync(SaveCommissionWorkDto dto, int createdByUserId);
    Task<CommissionWorkDto?> UpdateAsync(int id, SaveCommissionWorkDto dto);
    Task<bool> DeleteAsync(int id);
}
