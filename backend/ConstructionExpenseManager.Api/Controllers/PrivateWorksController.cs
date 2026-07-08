using ConstructionExpenseManager.Api.Common;
using ConstructionExpenseManager.Api.DTOs.PrivateWorks;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionExpenseManager.Api.Controllers;

[ApiController]
[Route("api/private-works")]
[Authorize]
public class PrivateWorksController : ControllerBase
{
    private readonly IPrivateWorkService _service;
    private readonly ICurrentUserService _currentUserService;

    public PrivateWorksController(IPrivateWorkService service, ICurrentUserService currentUserService)
    {
        _service = service;
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
    public async Task<IActionResult> Create([FromBody] SavePrivateWorkDto dto)
    {
        var created = await _service.CreateAsync(dto, _currentUserService.UserId);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SavePrivateWorkDto dto)
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

    // ----- Milestones -----

    [HttpPost("{privateWorkId:int}/milestones")]
    public async Task<IActionResult> AddMilestone(int privateWorkId, [FromBody] SavePrivateWorkMilestoneDto dto)
    {
        var milestone = await _service.AddMilestoneAsync(privateWorkId, dto);
        return milestone is null ? NotFound() : Ok(milestone);
    }

    [HttpPut("{privateWorkId:int}/milestones/{milestoneId:int}")]
    public async Task<IActionResult> UpdateMilestone(int privateWorkId, int milestoneId, [FromBody] SavePrivateWorkMilestoneDto dto)
    {
        var milestone = await _service.UpdateMilestoneAsync(privateWorkId, milestoneId, dto);
        return milestone is null ? NotFound() : Ok(milestone);
    }

    [HttpDelete("{privateWorkId:int}/milestones/{milestoneId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteMilestone(int privateWorkId, int milestoneId)
    {
        var success = await _service.DeleteMilestoneAsync(privateWorkId, milestoneId);
        return success ? NoContent() : NotFound();
    }

    // ----- Categories -----

    [HttpPost("{privateWorkId:int}/categories")]
    public async Task<IActionResult> AddCategory(int privateWorkId, [FromBody] SavePrivateWorkCategoryDto dto)
    {
        var category = await _service.AddCategoryAsync(privateWorkId, dto);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpPut("{privateWorkId:int}/categories/{categoryId:int}")]
    public async Task<IActionResult> UpdateCategory(int privateWorkId, int categoryId, [FromBody] SavePrivateWorkCategoryDto dto)
    {
        var category = await _service.UpdateCategoryAsync(privateWorkId, categoryId, dto);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpDelete("{privateWorkId:int}/categories/{categoryId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteCategory(int privateWorkId, int categoryId)
    {
        var success = await _service.DeleteCategoryAsync(privateWorkId, categoryId);
        return success ? NoContent() : NotFound();
    }

    // ----- Category Payments -----

    [HttpPost("{privateWorkId:int}/categories/{categoryId:int}/payments")]
    public async Task<IActionResult> AddCategoryPayment(int privateWorkId, int categoryId, [FromBody] SavePrivateWorkCategoryPaymentDto dto)
    {
        var payment = await _service.AddCategoryPaymentAsync(privateWorkId, categoryId, dto);
        return payment is null ? NotFound() : Ok(payment);
    }

    [HttpPut("{privateWorkId:int}/categories/{categoryId:int}/payments/{paymentId:int}")]
    public async Task<IActionResult> UpdateCategoryPayment(int privateWorkId, int categoryId, int paymentId, [FromBody] SavePrivateWorkCategoryPaymentDto dto)
    {
        var payment = await _service.UpdateCategoryPaymentAsync(privateWorkId, categoryId, paymentId, dto);
        return payment is null ? NotFound() : Ok(payment);
    }

    [HttpDelete("{privateWorkId:int}/categories/{categoryId:int}/payments/{paymentId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteCategoryPayment(int privateWorkId, int categoryId, int paymentId)
    {
        var success = await _service.DeleteCategoryPaymentAsync(privateWorkId, categoryId, paymentId);
        return success ? NoContent() : NotFound();
    }

    // ----- Materials -----

    [HttpPost("{privateWorkId:int}/materials")]
    public async Task<IActionResult> AddMaterial(int privateWorkId, [FromBody] SavePrivateWorkMaterialDto dto)
    {
        var material = await _service.AddMaterialAsync(privateWorkId, dto);
        return material is null ? NotFound() : Ok(material);
    }

    [HttpPut("{privateWorkId:int}/materials/{materialId:int}")]
    public async Task<IActionResult> UpdateMaterial(int privateWorkId, int materialId, [FromBody] SavePrivateWorkMaterialDto dto)
    {
        var material = await _service.UpdateMaterialAsync(privateWorkId, materialId, dto);
        return material is null ? NotFound() : Ok(material);
    }

    [HttpDelete("{privateWorkId:int}/materials/{materialId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteMaterial(int privateWorkId, int materialId)
    {
        var success = await _service.DeleteMaterialAsync(privateWorkId, materialId);
        return success ? NoContent() : NotFound();
    }

    // ----- Departmental Labour -----

    [HttpPost("{privateWorkId:int}/departmental-labour")]
    public async Task<IActionResult> AddDepartmentalLabour(int privateWorkId, [FromBody] SavePrivateWorkDepartmentalLabourDto dto)
    {
        var entry = await _service.AddDepartmentalLabourAsync(privateWorkId, dto);
        return entry is null ? NotFound() : Ok(entry);
    }

    [HttpPut("{privateWorkId:int}/departmental-labour/{departmentalLabourId:int}")]
    public async Task<IActionResult> UpdateDepartmentalLabour(int privateWorkId, int departmentalLabourId, [FromBody] SavePrivateWorkDepartmentalLabourDto dto)
    {
        var entry = await _service.UpdateDepartmentalLabourAsync(privateWorkId, departmentalLabourId, dto);
        return entry is null ? NotFound() : Ok(entry);
    }

    [HttpDelete("{privateWorkId:int}/departmental-labour/{departmentalLabourId:int}")]
    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    public async Task<IActionResult> DeleteDepartmentalLabour(int privateWorkId, int departmentalLabourId)
    {
        var success = await _service.DeleteDepartmentalLabourAsync(privateWorkId, departmentalLabourId);
        return success ? NoContent() : NotFound();
    }
}
