

namespace gbs.Server.Repository.AuthRepository;

public interface IAuthRepository
{
    Task<ServiceResponse<string>> Register(RegisterDto request);
    Task<bool> UserExists(string email);
    Task<ServiceResponse<string>> Login(string email, string password);
    Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
    Task<ServiceResponse<List<RolesResponse>>> GetRoles();
    string GetUserId();
    List<string> GetUserRoles();
    bool UserIsAdmin();
}