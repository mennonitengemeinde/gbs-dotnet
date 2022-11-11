using Microsoft.AspNetCore.Identity;

namespace Gbs.Infrastructure.Persistence.Identity;

public class UserRole : IdentityUserRole<string>
{
    public virtual User User { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}