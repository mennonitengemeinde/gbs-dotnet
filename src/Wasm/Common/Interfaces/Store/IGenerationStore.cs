using Gbs.Shared.Generations;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IGenerationStore : IStore<GenerationResponse, int, CreateGenerationRequest, UpdateGenerationRequest> { }