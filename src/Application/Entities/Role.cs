using Microsoft.AspNetCore.Identity;

namespace Gbs.Application.Entities;

public class Role: IdentityRole
{
    public ICollection<UserRole> UserRoles { get; set; } = null!;
}