namespace Gbs.Wasm.Common.Interfaces.Api;

public interface IStreamService : IBaseApiService<StreamResponse>,
    IApiCrud<CreateStreamRequest, CreateStreamRequest, int>
{
    Task<StreamResponse?> GetById(ComponentBase sender, int id);
    Task<StreamResponse?> GetOnlyLiveById(ComponentBase sender, int id);
    Task ToggleLive(ComponentBase sender, int id);
}