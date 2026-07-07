using ConstructionExpenseManager.Api.Common;
using ConstructionExpenseManager.Api.DTOs.Auth;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Repositories;

namespace ConstructionExpenseManager.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponseDto?> LoginAsync(string mobileNumber)
    {
        var user = await _userRepository.GetByMobileNumberAsync(mobileNumber.Trim());
        if (user is null || !user.IsActive)
        {
            return null;
        }

        return new LoginResponseDto
        {
            Token = _jwtTokenService.GenerateToken(user),
            User = ToDto(user)
        };
    }

    public async Task<UserDto?> GetCurrentUserAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user is null ? null : ToDto(user);
    }

    private static UserDto ToDto(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        MobileNumber = user.MobileNumber,
        Role = user.Role.ToString(),
        IsActive = user.IsActive
    };
}
