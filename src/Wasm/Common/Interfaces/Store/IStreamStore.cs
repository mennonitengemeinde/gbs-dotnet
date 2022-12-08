using Gbs.Shared.Streams;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IStreamStore : IStore<StreamResponse, int, CreateStreamRequest, UpdateStreamRequest>
{
    Task<StreamResponse?> GetOnlyLiveById(int id);
    Task ToggleLive(int id);
}