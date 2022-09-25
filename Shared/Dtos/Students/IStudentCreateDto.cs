using gbs.Shared.Enums;

namespace gbs.Shared.Dtos.Students;

public interface IStudentCreateDto
{
    public string Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int? ChurchId { get; set; }
}