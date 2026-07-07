using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.Users;

public class CreateUserDto
{
    [Required, MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(15)]
    public string MobileNumber { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = "Manager";
}
