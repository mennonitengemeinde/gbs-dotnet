using System.Security.Claims;

namespace gbs.Client.Services.Api.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<string>> Register(RegisterDto userRegister);
    Task<ServiceResponse<string>> Login(LoginDto userLogin);
    // Task<ServiceResponse<bool>> ChangePassword(PasswordC changePasswordDto);
    Task<ServiceResponse<List<RolesResponse>>> FetchRoles();
    Task<bool> IsUserAuthenticated();
    Task<List<string>> GetUserRoles();
    Task<bool> UserIsAdmin();
}