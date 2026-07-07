using ConstructionExpenseManager.Api.DTOs.TenderWorks;

namespace ConstructionExpenseManager.Api.Services;

public interface ITenderWorkService
{
    Task<List<TenderWorkDto>> GetAllAsync();
    Task<TenderWorkDto?> GetByIdAsync(int id);
    Task<TenderWorkDto> CreateAsync(SaveTenderWorkDto dto, int createdByUserId);
    Task<TenderWorkDto?> UpdateAsync(int id, SaveTenderWorkDto dto);
    Task<bool> DeleteAsync(int id);

    Task<List<TenderRaBillDto>> GetRaBillsAsync(int tenderWorkId);
    Task<TenderRaBillDto?> AddRaBillAsync(int tenderWorkId, SaveTenderRaBillDto dto);
    Task<TenderRaBillDto?> UpdateRaBillAsync(int tenderWorkId, int raBillId, SaveTenderRaBillDto dto);
    Task<bool> DeleteRaBillAsync(int tenderWorkId, int raBillId);
}
