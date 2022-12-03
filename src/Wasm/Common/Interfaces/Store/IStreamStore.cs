using Gbs.Shared.Streams;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IStreamStore : IStore<StreamDto, int, StreamCreateDto, StreamCreateDto>
{
    Task<StreamDto?> GetOnlyLiveById(int id);
    Task ToggleLive(int id);
}