using ConstructionExpenseManager.Api.Common;
using ConstructionExpenseManager.Api.DTOs.Auth;
using ConstructionExpenseManager.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
namespace ConstructionExpenseManager.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ICurrentUserService _currentUserService;

    public AuthController(IAuthService authService, ICurrentUserService currentUserService)
    {
        _authService = authService;
        _currentUserService = currentUserService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request.MobileNumber);
        if (result is null)
        {
            return Unauthorized(new { message = "Mobile number not recognized or account is inactive." });
        }

        return Ok(result);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> Me()
    {
        var user = await _authService.GetCurrentUserAsync(_currentUserService.UserId);
        return user is null ? NotFound() : Ok(user);
    }
}
