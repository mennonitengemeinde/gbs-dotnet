namespace gbs.Client.Services.Api.UserService;

public interface IUserService
{
    List<UserDto> Users { get; set; }
    event Action UsersChanged;
    Task GetUsers();
    Task<ServiceResponse<List<UserDto>>> FetchUsers();
    Task UpdateRole(int userId, UserUpdateRoleDto userUpdateRoleDto);
}