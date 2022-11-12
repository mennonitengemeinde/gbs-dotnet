using Microsoft.AspNetCore.Identity;

namespace Gbs.Infrastructure.Persistence.Identity;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }

    public int? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    
    public int? ChurchId { get; set; }
    public Church? Church { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; } = null!;
}