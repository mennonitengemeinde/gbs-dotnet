namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IStore<T, in TId>
{
    public string BaseUrl { get; }
    public bool HasError { get; }
    public string? ErrorMessage { get; }
    public bool IsLoading { get; }
    event Action OnChange;
    List<T> Data { get; }
    DateTime LastUpdated { get; }
    Task Fetch();
    void CacheData(List<T> value);
    void ClearErrors();
    Task ForceFetch();
    T? GetByIdQuery(TId id);
    Task<T?> GetById(TId id);
    Task Delete(TId id);
}

public interface IStore<T, in TId, in TCreate, in TUpdate> : IStore<T, TId>
{
    Task Add(TCreate item);
    Task Update(TId id, TUpdate item);
}