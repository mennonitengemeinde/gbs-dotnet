using System.ComponentModel.DataAnnotations;

namespace gbs.Shared.Dtos;

public class RegisterDto
{
    [Display(Name = "First Name")]
    [Required, StringLength(100, MinimumLength = 3)]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")]
    [Required, StringLength(100, MinimumLength = 3)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress] public string Email { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "The passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}