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
        var result = await _http.GetFromJsonAsync<ServiceResponse<List<UserDto>>>("api/users");
        if (result?.Data != null)
        {
            Users = result.Data;
        }
        UsersChanged?.Invoke();
    }

    public async Task UpdateRole(int userId, UserUpdateRoleDto userUpdateRoleDto)
    {
        var response = await _http.PutAsJsonAsync($"api/users/{userId}/role", userUpdateRoleDto);
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<UserDto>>();
        if (result?.Success == true)
        {
            await GetUsers();
            UsersChanged?.Invoke();
        }
    }
}