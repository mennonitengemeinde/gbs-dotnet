using Gbs.Shared.Churches;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IChurchStore : IStore<ChurchResponse, int, CreateChurchRequest, CreateChurchRequest>
{
    
}