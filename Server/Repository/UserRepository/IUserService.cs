namespace gbs.Server.Repository.UserRepository;

public interface IUserRepository
{
    Task<ServiceResponse<List<UserDto>>> GetUsers();
    Task<ServiceResponse<UserDto>> UpdateUserRole(int userId, string newRole);
}