using Gbs.Wasm.Common.Models;

namespace Gbs.Wasm.Store.ChurchUseCase;

public record FetchChurchesAction();

public record FetchChurchesActionResult(IEnumerable<ChurchResponse>? Churches, ApiError? Error);
