namespace gbs.Client.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(RegisterDto userRegister);
    Task<ServiceResponse<string>> Login(LoginDto userLogin);
    // Task<ServiceResponse<bool>> ChangePassword(PasswordC changePasswordDto);
    Task<bool> IsUserAuthenticated();
}