namespace Gbs.Client.Wasm.Services.Api.AuthService;

public interface IAuthService
{
    Task<Result<string>> Register(RegisterDto userRegister);
    Task<Result<string>> Login(LoginDto userLogin);
    // Task<ServiceResponse<bool>> ChangePassword(PasswordC changePasswordDto);
    Task<Result<List<RolesDto>>> FetchRoles();
    Task<bool> IsUserAuthenticated();
    Task<List<string>> GetUserRoles();
    Task<bool> UserIsAdmin();
}