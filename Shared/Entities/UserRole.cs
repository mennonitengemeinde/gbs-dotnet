using Microsoft.AspNetCore.Identity;

namespace gbs.Shared.Entities;

public class UserRole : IdentityUserRole<string>
{
    public virtual User User { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}