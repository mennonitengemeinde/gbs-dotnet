namespace Gbs.Core.Domain.Dto.Students;

public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public MaritalStatus MaritalStatus { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public int ChurchId { get; set; }
    public ChurchDto Church { get; set; } = null!;

    public string? UserId { get; set; }
    
    public ICollection<EnrollmentDto> Enrollments { get; set; } = new List<EnrollmentDto>();
}