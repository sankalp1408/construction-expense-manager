using System.ComponentModel.DataAnnotations;

namespace ConstructionExpenseManager.Api.DTOs.Auth;

public class LoginRequestDto
{
    [Required]
    public string MobileNumber { get; set; } = string.Empty;
}
