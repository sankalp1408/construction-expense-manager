using ConstructionExpenseManager.Api.DTOs.Common;
using ConstructionExpenseManager.Api.Models;

namespace ConstructionExpenseManager.Api.Services;

public interface IGstVendorLedgerService
{
    Task<GstVendorLedgerDto> GetLedgerAsync(GstWorkType workType, int workId);
    Task<GstVendorEntryDto> AddEntryAsync(GstWorkType workType, int workId, SaveGstVendorEntryDto dto);
    Task<GstVendorEntryDto?> UpdateEntryAsync(GstWorkType workType, int workId, int entryId, SaveGstVendorEntryDto dto);
    Task<bool> DeleteEntryAsync(GstWorkType workType, int workId, int entryId);

    Task<GstVendorRepaymentDto?> AddRepaymentAsync(GstWorkType workType, int workId, int entryId, SaveGstVendorRepaymentDto dto);
    Task<GstVendorRepaymentDto?> UpdateRepaymentAsync(GstWorkType workType, int workId, int entryId, int repaymentId, SaveGstVendorRepaymentDto dto);
    Task<bool> DeleteRepaymentAsync(GstWorkType workType, int workId, int entryId, int repaymentId);
}
