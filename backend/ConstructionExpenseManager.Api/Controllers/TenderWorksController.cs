using ConstructionExpenseManager.Api.Common;
using ConstructionExpenseManager.Api.DTOs.Common;
using ConstructionExpenseManager.Api.DTOs.TenderWorks;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionExpenseManager.Api.Controllers;

[ApiController]
[Route("api/tender-works")]
[Authorize]
public class TenderWorksController : ControllerBase
{
    private readonly ITenderWorkService _service;
    private readonly IGstVendorLedgerService _gstLedgerService;
    private readonly ICurrentUserService _currentUserService;

    public TenderWorksController(ITenderWorkService service, IGstVendorLedgerService gstLedgerService, ICurrentUserService currentUserService)
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
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> Create([FromBody] SaveTenderWorkDto dto)
    {
        var created = await _service.CreateAsync(dto, _currentUserService.UserId);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveTenderWorkDto dto)
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

    // ----- RA Bills -----

    [HttpGet("{tenderWorkId:int}/ra-bills")]
    public async Task<IActionResult> GetRaBills(int tenderWorkId) => Ok(await _service.GetRaBillsAsync(tenderWorkId));

    [HttpPost("{tenderWorkId:int}/ra-bills")]
    public async Task<IActionResult> AddRaBill(int tenderWorkId, [FromBody] SaveTenderRaBillDto dto)
    {
        var bill = await _service.AddRaBillAsync(tenderWorkId, dto);
        return bill is null ? NotFound() : Ok(bill);
    }

    [HttpPut("{tenderWorkId:int}/ra-bills/{raBillId:int}")]
    public async Task<IActionResult> UpdateRaBill(int tenderWorkId, int raBillId, [FromBody] SaveTenderRaBillDto dto)
    {
        var bill = await _service.UpdateRaBillAsync(tenderWorkId, raBillId, dto);
        return bill is null ? NotFound() : Ok(bill);
    }

    [HttpDelete("{tenderWorkId:int}/ra-bills/{raBillId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteRaBill(int tenderWorkId, int raBillId)
    {
        var success = await _service.DeleteRaBillAsync(tenderWorkId, raBillId);
        return success ? NoContent() : NotFound();
    }

    // ----- GST Vendor Sub-ledger -----

    [HttpGet("{tenderWorkId:int}/gst-vendors")]
    public async Task<IActionResult> GetGstVendors(int tenderWorkId) =>
        Ok(await _gstLedgerService.GetLedgerAsync(GstWorkType.Tender, tenderWorkId));

    [HttpPost("{tenderWorkId:int}/gst-vendors")]
    public async Task<IActionResult> AddGstVendor(int tenderWorkId, [FromBody] SaveGstVendorEntryDto dto) =>
        Ok(await _gstLedgerService.AddEntryAsync(GstWorkType.Tender, tenderWorkId, dto));

    [HttpPut("{tenderWorkId:int}/gst-vendors/{entryId:int}")]
    public async Task<IActionResult> UpdateGstVendor(int tenderWorkId, int entryId, [FromBody] SaveGstVendorEntryDto dto)
    {
        var entry = await _gstLedgerService.UpdateEntryAsync(GstWorkType.Tender, tenderWorkId, entryId, dto);
        return entry is null ? NotFound() : Ok(entry);
    }

    [HttpDelete("{tenderWorkId:int}/gst-vendors/{entryId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteGstVendor(int tenderWorkId, int entryId)
    {
        var success = await _gstLedgerService.DeleteEntryAsync(GstWorkType.Tender, tenderWorkId, entryId);
        return success ? NoContent() : NotFound();
    }

    // ----- GST Vendor Repayments -----

    [HttpPost("{tenderWorkId:int}/gst-vendors/{entryId:int}/repayments")]
    public async Task<IActionResult> AddGstVendorRepayment(int tenderWorkId, int entryId, [FromBody] SaveGstVendorRepaymentDto dto)
    {
        var repayment = await _gstLedgerService.AddRepaymentAsync(GstWorkType.Tender, tenderWorkId, entryId, dto);
        return repayment is null ? NotFound() : Ok(repayment);
    }

    [HttpPut("{tenderWorkId:int}/gst-vendors/{entryId:int}/repayments/{repaymentId:int}")]
    public async Task<IActionResult> UpdateGstVendorRepayment(int tenderWorkId, int entryId, int repaymentId, [FromBody] SaveGstVendorRepaymentDto dto)
    {
        var repayment = await _gstLedgerService.UpdateRepaymentAsync(GstWorkType.Tender, tenderWorkId, entryId, repaymentId, dto);
        return repayment is null ? NotFound() : Ok(repayment);
    }

    [HttpDelete("{tenderWorkId:int}/gst-vendors/{entryId:int}/repayments/{repaymentId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteGstVendorRepayment(int tenderWorkId, int entryId, int repaymentId)
    {
        var success = await _gstLedgerService.DeleteRepaymentAsync(GstWorkType.Tender, tenderWorkId, entryId, repaymentId);
        return success ? NoContent() : NotFound();
    }
}
