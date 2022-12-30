namespace Gbs.Application.Features.Identity.Interfaces;

public interface IIdentityCommands
{
    Task<Result<string>> Login(string email, string password);
    Task<Result<string>> Add(RegisterRequest request);
    Task<Result<UserResponse>> UpdateRoles(string id, List<string> request);
    Task<Result<UserResponse>> UpdateActiveState(string id, bool request);
    Task<Result<UserResponse>> UpdateChurch(string id, UpdateUserChurchRequest request);
    Task<Result<bool>> Delete(string id);
}