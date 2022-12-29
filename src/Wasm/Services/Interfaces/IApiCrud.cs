namespace Gbs.Wasm.Services.Interfaces;

public interface IApiCrud<in TCreate, in TUpdate, in TId>
{
    string BaseUrl { get; }
    Task Fetch(int minutes);
    Task ForceFetch();
    Task Create(TCreate request);
    Task Update(TId id, TUpdate request);
    Task Delete(TId id);
}