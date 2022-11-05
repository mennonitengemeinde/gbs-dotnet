namespace Gbs.Server.Application.Common.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<Result<string>> Register(RegisterDto request);
    Task<bool> UserExists(string email);
    Task<Result<string>> Login(string email, string password);
    Task<Result<bool>> ChangePassword(int userId, string newPassword);
    Task<Result<List<RolesDto>>> GetRoles();
    // string GetUserId();
    // List<string> GetUserRoles();
    // bool UserIsAdmin();
}