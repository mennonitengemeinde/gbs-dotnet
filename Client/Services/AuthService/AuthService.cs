namespace gbs.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
    {
        _http = http;
        _authStateProvider = authStateProvider;
    }
    
    public async Task<ServiceResponse<int>> Register(RegisterDto userRegister)
    {
        var result = await _http.PostAsJsonAsync("api/auth/register", userRegister);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    public async Task<ServiceResponse<string>> Login(LoginDto userLogin)
    {
        var result = await _http.PostAsJsonAsync("api/auth/login", userLogin);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated;
    }
}