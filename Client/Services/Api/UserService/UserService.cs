namespace gbs.Client.Services.Api.UserService;

public class UserService : IUserService
{
    private readonly HttpClient _http;
    public List<UserDto> Users { get; set; } = new List<UserDto>();
    public event Action? UsersChanged;

    public UserService(HttpClient http)
    {
        _http = http;
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
}