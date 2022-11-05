using Microsoft.AspNetCore.Identity;

namespace Gbs.Server.Persistence.Identity;

public class Role: IdentityRole
{
    public ICollection<UserRole> UserRoles { get; set; } = null!;
}