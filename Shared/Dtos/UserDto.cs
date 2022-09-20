using gbs.Shared.Const;
using gbs.Shared.Entities;

namespace gbs.Shared.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool EmailVerified { get; set; } = false;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = Roles.User;
    public bool IsActive { get; set; } = false;
    public int? ChurchId { get; set; }
    public Church? Church { get; set; }
}