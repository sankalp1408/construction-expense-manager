using ConstructionExpenseManager.Api.Common;
using ConstructionExpenseManager.Api.DTOs.CommissionWorks;
using ConstructionExpenseManager.Api.DTOs.Common;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionExpenseManager.Api.Controllers;

[ApiController]
[Route("api/commission-works")]
[Authorize]
public class CommissionWorksController : ControllerBase
{
    private readonly ICommissionWorkService _service;
    private readonly IGstVendorLedgerService _gstLedgerService;
    private readonly ICurrentUserService _currentUserService;

    public CommissionWorksController(ICommissionWorkService service, IGstVendorLedgerService gstLedgerService, ICurrentUserService currentUserService)
    {
        _service = service;
        _gstLedgerService = gstLedgerService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var work = await _service.GetByIdAsync(id);
        return work is null ? NotFound() : Ok(work);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveCommissionWorkDto dto)
    {
        var created = await _service.CreateAsync(dto, _currentUserService.UserId);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveCommissionWorkDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }

    // ----- GST Vendor Sub-ledger (shared with Tender module) -----

    [HttpGet("{commissionWorkId:int}/gst-vendors")]
    public async Task<IActionResult> GetGstVendors(int commissionWorkId) =>
        Ok(await _gstLedgerService.GetLedgerAsync(GstWorkType.Commission, commissionWorkId));

    [HttpPost("{commissionWorkId:int}/gst-vendors")]
    public async Task<IActionResult> AddGstVendor(int commissionWorkId, [FromBody] SaveGstVendorEntryDto dto) =>
        Ok(await _gstLedgerService.AddEntryAsync(GstWorkType.Commission, commissionWorkId, dto));

    [HttpPut("{commissionWorkId:int}/gst-vendors/{entryId:int}")]
    public async Task<IActionResult> UpdateGstVendor(int commissionWorkId, int entryId, [FromBody] SaveGstVendorEntryDto dto)
    {
        var entry = await _gstLedgerService.UpdateEntryAsync(GstWorkType.Commission, commissionWorkId, entryId, dto);
        return entry is null ? NotFound() : Ok(entry);
    }

    [HttpDelete("{commissionWorkId:int}/gst-vendors/{entryId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteGstVendor(int commissionWorkId, int entryId)
    {
        var success = await _gstLedgerService.DeleteEntryAsync(GstWorkType.Commission, commissionWorkId, entryId);
        return success ? NoContent() : NotFound();
    }

    // ----- GST Vendor Repayments -----

    [HttpPost("{commissionWorkId:int}/gst-vendors/{entryId:int}/repayments")]
    public async Task<IActionResult> AddGstVendorRepayment(int commissionWorkId, int entryId, [FromBody] SaveGstVendorRepaymentDto dto)
    {
        var repayment = await _gstLedgerService.AddRepaymentAsync(GstWorkType.Commission, commissionWorkId, entryId, dto);
        return repayment is null ? NotFound() : Ok(repayment);
    }

    [HttpPut("{commissionWorkId:int}/gst-vendors/{entryId:int}/repayments/{repaymentId:int}")]
    public async Task<IActionResult> UpdateGstVendorRepayment(int commissionWorkId, int entryId, int repaymentId, [FromBody] SaveGstVendorRepaymentDto dto)
    {
        var repayment = await _gstLedgerService.UpdateRepaymentAsync(GstWorkType.Commission, commissionWorkId, entryId, repaymentId, dto);
        return repayment is null ? NotFound() : Ok(repayment);
    }

    [HttpDelete("{commissionWorkId:int}/gst-vendors/{entryId:int}/repayments/{repaymentId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteGstVendorRepayment(int commissionWorkId, int entryId, int repaymentId)
    {
        var success = await _gstLedgerService.DeleteRepaymentAsync(GstWorkType.Commission, commissionWorkId, entryId, repaymentId);
        return success ? NoContent() : NotFound();
    }
}
