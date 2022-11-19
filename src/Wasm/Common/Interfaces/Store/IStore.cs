namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IStore<T>
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
    T? GetByIdQuery(int id);
    Task<T?> GetById(int id);
    Task Delete(int id);
}

public interface IStore<T, in TCreate, in TUpdate> : IStore<T>
{
    Task Add(TCreate item);
    Task Update(int id, TUpdate item);
}