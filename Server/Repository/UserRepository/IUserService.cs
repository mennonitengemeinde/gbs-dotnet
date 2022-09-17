namespace gbs.Server.Repository.UserRepository;

public interface IUserRepository
{
    Task<ServiceResponse<List<UserDto>>> GetUsers();
    Task<ServiceResponse<UserDto>> GetUserById(int userId);
    Task<ServiceResponse<UserDto>> UpdateUserRole(int userId, string newRole);
    Task<ServiceResponse<UserDto>> UpdateUserActiveState(int userId, bool newActiveState);
}