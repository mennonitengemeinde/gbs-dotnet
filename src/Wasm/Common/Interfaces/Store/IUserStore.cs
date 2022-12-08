using Gbs.Shared.Identity;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IUserStore : IStore<UserResponse, string, RegisterRequest, RegisterRequest>
{
    Task UpdateChurch(string id, UpdateUserChurchRequest request);
    Task UpdateRoles(string id, UpdateUserRoleRequest request);
    Task UpdateActiveState(string id, UpdateUserActiveStateRequest request);
}