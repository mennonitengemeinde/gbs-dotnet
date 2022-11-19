namespace Gbs.Application.Common.Interfaces.Queries;

public interface IUserQueries
{
    Task<Result<List<UserDto>>> GetAll();
    Task<Result<UserDto>> GetById(string id);
}