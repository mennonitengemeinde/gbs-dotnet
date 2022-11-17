using System.ComponentModel.DataAnnotations;

namespace Gbs.Shared.Dto.Identity;

public class LoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}