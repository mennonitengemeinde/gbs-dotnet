namespace gbs.Client.Services.Api.AuthService;

public class AuthApiService : IAuthService
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
        return await _http.PostAsJsonAsync("api/auth/register", userRegister)
            .EnsureSuccess<int>();
    }

    public async Task<ServiceResponse<string>> Login(LoginDto userLogin)
    {
        return await _http.PostAsJsonAsync("api/auth/login", userLogin)
            .EnsureSuccess<string>();
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated;
    }
}