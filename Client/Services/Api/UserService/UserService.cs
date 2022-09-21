namespace gbs.Client.Services.Api.UserService;

public class UserService : IUserService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<UserDto> Users { get; set; } = new List<UserDto>();
    public event Action? UsersChanged;

    public UserService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
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
            .EnsureSuccess<UserDto>();
        if (response.Success)
        {
            await GetUsers();
            UsersChanged?.Invoke();
        }
    }

    public async Task UpdateActiveState(int userId, UserUpdateActiveStateDto userUpdateActiveDto)
    {
        var response = await _http.PutAsJsonAsync($"api/users/{userId}/active", userUpdateActiveDto)
            .EnsureSuccess<UserDto>();
        if (response.Success)
        {
            await GetUsers();
            UsersChanged?.Invoke();
        }
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