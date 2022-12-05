using Gbs.Shared.Churches;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IChurchStore : IStore<ChurchDto, int, CreateChurchRequest, CreateChurchRequest> { }