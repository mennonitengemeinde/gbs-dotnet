using Microsoft.AspNetCore.Identity;

namespace Gbs.Infrastructure.Persistence.Identity;

public class Role: IdentityRole
{
    public ICollection<UserRole> UserRoles { get; set; } = null!;
}