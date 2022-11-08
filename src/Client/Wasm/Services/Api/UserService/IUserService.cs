namespace gbs.Client.Wasm.Services.Api.UserService;

public interface IUserService
{
    List<UserDto> Users { get; set; }
    event Action UsersChanged;
    Task GetUsers();
    Task<ServiceResponse<List<UserDto>>> FetchUsers();
    Task UpdateChurch(string userId, UserUpdateChurchDto updateDto);
    Task UpdateRole(string userId, UserUpdateRoleDto userUpdateRoleDto);
    Task UpdateActiveState(string userId, UserUpdateActiveStateDto userUpdateActiveDto);
}