using ConstructionExpenseManager.Api.DTOs.Auth;
using ConstructionExpenseManager.Api.DTOs.Users;

namespace ConstructionExpenseManager.Api.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(CreateUserDto dto);
    Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto);
    Task<bool> DeactivateAsync(int id);
}
