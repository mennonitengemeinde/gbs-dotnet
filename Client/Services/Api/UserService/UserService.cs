using gbs.Shared.Const;

namespace gbs.Client.Services.Api.UserService;

public class UserService : IUserService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    private readonly AuthenticationStateProvider _authStateProvider;
    public List<UserDto> Users { get; set; } = new List<UserDto>();
    public event Action? UsersChanged;

    public UserService(HttpClient http, IUiService uiService, AuthenticationStateProvider authStateProvider)
    {
        _http = http;
        _uiService = uiService;
        _authStateProvider = authStateProvider;
    }

    public async Task GetUsers()
    {
        var result = await FetchUsers();
        if (result.Success)
            Users = result.Data;

        UsersChanged?.Invoke();
    }

    public async Task<ServiceResponse<List<UserDto>>> FetchUsers()
    {
        return await _http
            .GetAsync("api/users")
            .EnsureSuccess<List<UserDto>>();
    }

    public async Task UpdateChurch(int userId, UserUpdateChurchDto updateDto)
    {
        var result = await _http
            .PutAsJsonAsync($"api/users/{userId}/church", updateDto)
            .EnsureSuccess<List<UserDto>>();
        await HandleUsersChanged(result);
    }

    public async Task UpdateRole(int userId, UserUpdateRoleDto userUpdateRoleDto)
    {
        var response = await _http.PutAsJsonAsync($"api/users/{userId}/role", userUpdateRoleDto)
            .EnsureSuccess<List<UserDto>>();
        await HandleUsersChanged(response);
    }

    public async Task UpdateActiveState(int userId, UserUpdateActiveStateDto userUpdateActiveDto)
    {
        var response = await _http.PutAsJsonAsync($"api/users/{userId}/active", userUpdateActiveDto)
            .EnsureSuccess<List<UserDto>>();
        await HandleUsersChanged(response);
    }

    public async Task<string> GetUserRole()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        var role = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
        foreach (var claim in user.Claims)
        {
            Console.WriteLine(claim.Type);
        }
        return role ?? Roles.User;
    }

    private async Task HandleUsersChanged(ServiceResponse<List<UserDto>> result)
    {
        if (result.Success)
        {
            Users = result.Data;
            UsersChanged?.Invoke();
        }
        else
        {
            await _uiService.ShowErrorAlert(result.Message, result.StatusCode);
        }
    }
}