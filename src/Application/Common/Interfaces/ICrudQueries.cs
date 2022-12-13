namespace Gbs.Application.Common.Interfaces;

public interface ICrudQueries<T>
{
    Task<Result<List<T>>> GetAll();
    Task<Result<T>> GetById(int id);
}