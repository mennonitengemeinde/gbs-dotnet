using System.Net;

namespace gbs.Client.Services.Api.AuthService;

public class AuthApiService : BaseApiService, IAuthService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthApiService(HttpClient http, AuthenticationStateProvider authStateProvider)
    {
        _http = http;
        _authStateProvider = authStateProvider;
    }

    public async Task<ServiceResponse<int>> Register(RegisterDto userRegister)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", userRegister);
        return await EnsureSuccess<int>(response);
    }

    public async Task<ServiceResponse<string>> Login(LoginDto userLogin)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", userLogin);
        return await EnsureSuccess<string>(response);
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated;
    }
}