using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.Users;

public class UpdateUserDto
{
    [Required, MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(15)]
    public string MobileNumber { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
