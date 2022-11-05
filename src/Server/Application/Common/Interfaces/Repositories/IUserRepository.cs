namespace Gbs.Server.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<Result<List<UserDto>>> GetUsers();
    Task<Result<UserDto>> GetUserById(string userId);
    Task<Result<List<UserDto>>> UpdateUserRole(string userId, List<string> newRoles);
    Task<Result<List<UserDto>>> UpdateUserActiveState(string userId, bool newActiveState);
    Task<Result<List<UserDto>>> UpdateUserChurch(string userId, UserUpdateChurchDto updateDto);
}