

namespace gbs.Server.Repository.AuthRepository;

public interface IAuthRepository
{
    Task<ServiceResponse<int>> Register(RegisterDto request);
    Task<bool> UserExists(string email);
    Task<ServiceResponse<string>> Login(string email, string password);
    Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
    int GetUserId();
    string GetUserRole();
    bool UserIsAdmin();
}