namespace gbs.Client.Services.Api.AuthService;

public interface IAuthService
{
    Task<int> Register(RegisterDto userRegister);
    Task<string> Login(LoginDto userLogin);
    // Task<ServiceResponse<bool>> ChangePassword(PasswordC changePasswordDto);
    Task<bool> IsUserAuthenticated();
}