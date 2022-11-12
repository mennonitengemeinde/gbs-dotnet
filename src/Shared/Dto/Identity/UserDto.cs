namespace Gbs.Shared.Dto.Identity;

public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public IEnumerable<string> Roles { get; set; } = null!;
    public bool IsActive { get; set; } = false;
    public int? ChurchId { get; set; }
    public string? ChurchName { get; set; }
}