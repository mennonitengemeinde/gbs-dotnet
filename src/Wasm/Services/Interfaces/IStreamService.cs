using Gbs.Shared.Streams;

namespace Gbs.Wasm.Services.Interfaces;

public interface IStreamService : IBaseApiService<StreamResponse>,
    IApiCrud<CreateStreamRequest, CreateStreamRequest, int>
{
    Task<StreamResponse?> GetById(int id);
    Task<StreamResponse?> GetOnlyLiveById(int id);
    Task ToggleLive(int id);
}