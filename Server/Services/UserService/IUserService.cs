namespace gbs.Server.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<List<UserDto>>> GetUsers();
    Task<ServiceResponse<UserDto>> UpdateUserRole(int userId, string newRole);
}