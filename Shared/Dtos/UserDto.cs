using gbs.Shared.Const;

namespace gbs.Shared.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool EmailVerified { get; set; } = false;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = Roles.User;
}