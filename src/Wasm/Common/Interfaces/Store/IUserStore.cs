using Gbs.Shared.Identity;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IUserStore : IStore<UserDto, string, RegisterDto, RegisterDto>
{
    Task UpdateChurch(string id, UserUpdateChurchDto request);
    Task UpdateRoles(string id, UserUpdateRoleDto request);
    Task UpdateActiveState(string id, UserUpdateActiveStateDto request);
}