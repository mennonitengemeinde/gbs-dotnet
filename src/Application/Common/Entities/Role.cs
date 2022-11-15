using Microsoft.AspNetCore.Identity;

namespace Gbs.Application.Common.Entities;

public class Role: IdentityRole
{
    public ICollection<UserRole> UserRoles { get; set; } = null!;
}