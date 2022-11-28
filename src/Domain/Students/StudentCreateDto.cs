using System.ComponentModel.DataAnnotations;

namespace Gbs.Domain.Students;

public class StudentCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Date of Birth")]
    [Required]
    public DateTime? DateOfBirth { get; set; }

    [StringLength(100, MinimumLength = 3)] public string? Address { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string City { get; set; } = string.Empty;

    [Display(Name = "Province/State")]
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string State { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Country { get; set; } = string.Empty;

    [Display(Name = "Postal Code")]
    [StringLength(10, MinimumLength = 3)]
    public string PostalCode { get; set; } = string.Empty;

    [Display(Name = "Marital Status")]
    [Required]
    public MaritalStatus MaritalStatus { get; set; }

    [EmailAddress]
    [StringLength(100, MinimumLength = 3)]
    public string? Email { get; set; }

    [StringLength(50, MinimumLength = 3)] public string? Phone { get; set; }
    
    [Display(Name = "Status")]
    [Required]
    public EnrollmentState Status { get; set; }
    
    public string? Testimony { get; set; }
    
    public bool AgreedToGbsConcept { get; set; } = false;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a church")]
    public int ChurchId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a generation")]
    public int GenerationId { get; set; }
}