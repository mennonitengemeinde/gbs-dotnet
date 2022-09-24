using System.ComponentModel.DataAnnotations;
using gbs.Shared.Enums;

namespace gbs.Shared.Dtos;

public class StudentCreateDto
{
    [Required, StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required] public DateOnly DateOfBirth { get; set; }

    [Required, StringLength(100, MinimumLength = 3)]
    public string Address { get; set; } = string.Empty;

    [Required, StringLength(50, MinimumLength = 3)]
    public string City { get; set; } = string.Empty;

    [Required, StringLength(50, MinimumLength = 3)]
    public string State { get; set; } = string.Empty;

    [Required, StringLength(50, MinimumLength = 3)]
    public string Country { get; set; } = string.Empty;

    [Required, StringLength(10, MinimumLength = 3)]
    public string PostalCode { get; set; } = string.Empty;

    [Required] public MaritalStatus MaritalStatus { get; set; }
    [StringLength(100, MinimumLength = 3)] public string Email { get; set; } = string.Empty;
    [StringLength(50, MinimumLength = 3)] public string Phone { get; set; } = string.Empty;
    [Required] public int ChurchId { get; set; }
}