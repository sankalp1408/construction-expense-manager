using ConstructionExpenseManager.Api.DTOs.Auth;
using ConstructionExpenseManager.Api.DTOs.Users;
using ConstructionExpenseManager.Api.Models;
using ConstructionExpenseManager.Api.Repositories;

namespace ConstructionExpenseManager.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.OrderBy(u => u.Role).ThenBy(u => u.Name).Select(ToDto).ToList();
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user is null ? null : ToDto(user);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var existing = await _userRepository.GetByMobileNumberAsync(dto.MobileNumber.Trim());
        if (existing is not null)
        {
            throw new InvalidOperationException("A user with this mobile number already exists.");
        }

        if (!Enum.TryParse<UserRole>(dto.Role, true, out var role))
        {
            throw new InvalidOperationException("Invalid role. Must be 'SuperAdmin' or 'Manager'.");
        }

        var user = new User
        {
            Name = dto.Name.Trim(),
            MobileNumber = dto.MobileNumber.Trim(),
            Role = role,
            IsActive = true
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return ToDto(user);
    }

    public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return null;
        }

        var duplicate = await _userRepository.GetByMobileNumberAsync(dto.MobileNumber.Trim());
        if (duplicate is not null && duplicate.Id != id)
        {
            throw new InvalidOperationException("Another user already uses this mobile number.");
        }

        user.Name = dto.Name.Trim();
        user.MobileNumber = dto.MobileNumber.Trim();
        user.IsActive = dto.IsActive;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();

        return ToDto(user);
    }

    public async Task<bool> DeactivateAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return false;
        }

        user.IsActive = false;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        return true;
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
