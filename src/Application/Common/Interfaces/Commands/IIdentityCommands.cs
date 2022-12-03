using Gbs.Application.Identity;
using Gbs.Domain.Common.Wrapper;
using Gbs.Shared.Identity;

namespace Gbs.Application.Common.Interfaces.Commands;

public interface IIdentityCommands
{
    Task<Result<string>> Login(string email, string password);
    Task<Result<string>> Add(RegisterDto request);
    Task<Result<UserDto>> UpdateRoles(string id, List<string> request);
    Task<Result<UserDto>> UpdateActiveState(string id, bool request);
    Task<Result<UserDto>> UpdateChurch(string id, UserUpdateChurchDto request);
    Task<Result<bool>> Delete(string id);
}