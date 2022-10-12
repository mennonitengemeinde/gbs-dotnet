using Microsoft.AspNetCore.Identity;

namespace gbs.Shared.Entities;

public class Role: IdentityRole
{
    public ICollection<UserRole> UserRoles { get; set; } = null!;
}