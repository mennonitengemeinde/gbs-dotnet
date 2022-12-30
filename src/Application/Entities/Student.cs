using Gbs.Shared.Common.Enums;

namespace Gbs.Application.Entities;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    public string? Address { get; set; }
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string? PostalCode { get; set; }
    public string Country { get; set; } = string.Empty;

    public MaritalStatus MaritalStatus { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string HomeChurch { get; set; } = string.Empty;
    public EnrollmentState EnrollmentStatus { get; set; } = EnrollmentState.Active;
    public string? Testimony { get; set; }
    public bool AgreedToGbsConcept { get; set; } = false;

    public int ChurchId { get; set; }
    public Church Church { get; set; } = null!;

    public int GenerationId { get; set; }
    public Generation Generation { get; set; } = null!;

    public string? UserId { get; set; }
    // public User? User { get; set; }

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}