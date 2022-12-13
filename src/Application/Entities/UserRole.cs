using Microsoft.AspNetCore.Identity;

namespace Gbs.Application.Entities;

public class UserRole : IdentityUserRole<string>
{
    public virtual User User { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}