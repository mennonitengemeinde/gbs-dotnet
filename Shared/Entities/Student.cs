using gbs.Shared.Enums;

namespace gbs.Shared.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public MaritalStatus MaritalStatus { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public int ChurchId { get; set; }
    public Church Church { get; set; } = null!;

    public int? UserId { get; set; }
    public User? User { get; set; }
}