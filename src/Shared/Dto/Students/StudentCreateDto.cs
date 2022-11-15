using System.ComponentModel.DataAnnotations;

namespace Gbs.Shared.Dto.Students;

public class StudentCreateDto
{
    [Required, StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Date of Birth")]
    [Required]
    public DateTime? DateOfBirth { get; set; }

    [Required, StringLength(100, MinimumLength = 3)]
    public string Address { get; set; } = string.Empty;

    [Required, StringLength(50, MinimumLength = 3)]
    public string City { get; set; } = string.Empty;

    [Display(Name = "Province/State")]
    [Required, StringLength(50, MinimumLength = 3)]
    public string State { get; set; } = string.Empty;

    [Required, StringLength(50, MinimumLength = 3)]
    public string Country { get; set; } = string.Empty;

    [Display(Name = "Postal Code")]
    [Required, StringLength(10, MinimumLength = 3)]
    public string PostalCode { get; set; } = string.Empty;

    [Display(Name = "Marital Status")]
    [Required]
    public MaritalStatus MaritalStatus { get; set; }

    [EmailAddress, StringLength(100, MinimumLength = 3)]
    public string Email { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 3)] public string Phone { get; set; } = string.Empty;
    
    [Required, Range(1, int.MaxValue, ErrorMessage = "Please select a church")]
    public int ChurchId { get; set; }
}