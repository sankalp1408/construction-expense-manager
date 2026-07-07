using ConstructionExpenseManager.Api.DTOs.Auth;

namespace ConstructionExpenseManager.Api.Services;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(string mobileNumber);
    Task<UserDto?> GetCurrentUserAsync(int userId);
}
