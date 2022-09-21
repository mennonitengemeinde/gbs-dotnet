namespace gbs.Server.Repository.UserRepository;

public interface IUserRepository
{
    Task<ServiceResponse<List<UserDto>>> GetUsers();
    Task<ServiceResponse<UserDto>> GetUserById(int userId);
    Task<ServiceResponse<List<UserDto>>> UpdateUserRole(int userId, string newRole);
    Task<ServiceResponse<List<UserDto>>> UpdateUserActiveState(int userId, bool newActiveState);
    Task<ServiceResponse<List<UserDto>>> UpdateUserChurch(int userId, UserUpdateChurchDto updateDto);
}