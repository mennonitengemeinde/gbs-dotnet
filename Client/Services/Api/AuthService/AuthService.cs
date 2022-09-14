namespace gbs.Client.Services.Api.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
    {
        _http = http;
        _authStateProvider = authStateProvider;
    }
    
    public async Task<int> Register(RegisterDto userRegister)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", userRegister);
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        if (result?.Data == null || result.Success == false)
        {
            throw new Exception(result?.Message);
        }
        return result.Data;
    }

    public async Task<string> Login(LoginDto userLogin)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", userLogin);
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        if (result?.Data == null || result.Success == false)
        {
            throw new Exception(result?.Message);
        }

        return result.Data;
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated;
    }
}