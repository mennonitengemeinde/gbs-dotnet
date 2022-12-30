namespace Gbs.Application.Features.Identity.Interfaces;

public interface IIdentityQueries
{
    Task<Result<List<UserResponse>>> GetAll();
    Task<Result<UserResponse>> GetById(string id);
    Task<Result<List<RolesResponse>>> GetRoles();
}