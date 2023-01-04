namespace Gbs.Wasm.Common.Interfaces.Api;

public interface IApiCrud<in TCreate, in TUpdate, in TId>
{
    string BaseUrl { get; }
    int CacheTime { get; }
    Task Fetch(ComponentBase sender);
    Task ForceFetch(ComponentBase sender);
    Task Create(ComponentBase sender, TCreate request);
    Task Update(ComponentBase sender, TId id, TUpdate request);
    Task Delete(ComponentBase sender, TId id);
}