using Gbs.Shared.Identity;

namespace Gbs.Wasm.Common.Interfaces;

public interface IAuthService
{
    Task<Result<string>> Login(LoginRequest userLogin);
    // Task<ServiceResponse<bool>> ChangePassword(PasswordC changePasswordDto);
    Task<Result<List<RolesResponse>>> FetchRoles();
    Task<bool> IsUserAuthenticated();
    Task<List<string>> GetUserRoles();
    Task<int> GetUserChurchId();
    Task<bool> UserIsAdmin();
}