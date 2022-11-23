namespace Gbs.Wasm.Common.Interfaces;

public interface IAuthService
{
    Task<Result<string>> Login(LoginDto userLogin);
    // Task<ServiceResponse<bool>> ChangePassword(PasswordC changePasswordDto);
    Task<Result<List<RolesDto>>> FetchRoles();
    Task<bool> IsUserAuthenticated();
    Task<List<string>> GetUserRoles();
    Task<bool> UserIsAdmin();
}