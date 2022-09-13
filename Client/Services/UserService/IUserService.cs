namespace gbs.Client.Services.UserService;

public interface IUserService
{
    List<UserDto> Users { get; set; }
    event Action UsersChanged;
    Task GetUsers();
    Task UpdateRole(int userId, UserUpdateRoleDto userUpdateRoleDto);
}