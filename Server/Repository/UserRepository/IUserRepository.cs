namespace gbs.Server.Repository.UserRepository;

public interface IUserRepository
{
    Task<ServiceResponse<List<UserDto>>> GetUsers();
    Task<ServiceResponse<UserDto>> GetUserById(string userId);
    Task<ServiceResponse<List<UserDto>>> UpdateUserRole(string userId, List<string> newRoles);
    Task<ServiceResponse<List<UserDto>>> UpdateUserActiveState(string userId, bool newActiveState);
    Task<ServiceResponse<List<UserDto>>> UpdateUserChurch(string userId, UserUpdateChurchDto updateDto);
}