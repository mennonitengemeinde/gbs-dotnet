using gbs.Shared.Const;
using gbs.Shared.Entities;

namespace gbs.Shared.Dtos;

public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new List<string>();
    public bool IsActive { get; set; } = false;
    public int? ChurchId { get; set; }
    public string? ChurchName { get; set; }
}