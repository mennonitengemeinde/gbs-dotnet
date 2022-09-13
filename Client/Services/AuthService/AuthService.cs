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
        var response = await _http.PostAsJsonAsync("api/auth/register", userRegister);
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        if (result == null)
        {
            return new ServiceResponse<int>
            {
                Data = 0,
                Success = false,
                Message = "Something went wrong"
            };
        }
        return result;
    }

    public async Task<ServiceResponse<string>> Login(LoginDto userLogin)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", userLogin);
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        if (result == null)
        {
            return new ServiceResponse<string>
            {
                Data = null,
                Success = false,
                Message = "Something went wrong"
            };
        }

        return result;
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated;
    }
}