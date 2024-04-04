using System.Security.Claims;
using Gbs.Application.Common.Interfaces.Services;
using Gbs.Shared.Common.Const;

namespace Gbs.Web.Services;

public class AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUserService
{
    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    }

    public List<string> GetUserRoles()
    {
        return httpContextAccessor.HttpContext!.User
            .FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
    }

    public bool UserIsAdmin()
    {
        var role = GetUserRoles();
        return role.Contains(Roles.Admin) || role.Contains(Roles.SuperAdmin);
    }
}