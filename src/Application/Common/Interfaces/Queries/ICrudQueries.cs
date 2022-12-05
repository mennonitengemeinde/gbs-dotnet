namespace Gbs.Application.Common.Interfaces.Queries;

public interface ICrudQueries<T>
{
    Task<Result<List<T>>> GetAll();
    Task<Result<T>> GetById(int id);
}