namespace Gbs.Application.Common.Interfaces.Services;

public interface IAuthenticatedUserService
{
    string GetUserId();
    List<string> GetUserRoles();
    bool UserIsAdmin();
}