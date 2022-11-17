namespace Gbs.Wasm.Common.Interfaces;

public interface IStore {}

public interface IStore<T> : IStore
{
    public string BaseUrl { get; }
    event Action OnChange;
    List<T> Value { get; }
    DateTime LastUpdated { get; }
    Task Fetch();
    T? GetByIdQuery(int id);
    Task<T?> GetById(int id);
    Task Delete(int id);
}

public interface IStore<T, in TCreate> : IStore<T>
{
    Task Add(TCreate item);
    Task Update(int id, TCreate item);
}

public interface IStore<T, in TCreate, in TUpdate>
{
    Task Add(TCreate item);
    Task Update(int id, TUpdate item);
}