using Gbs.Wasm.Common.Models;

namespace Gbs.Wasm.Store.ChurchUseCase.Actions;

public class FetchChurchesResultAction
{
    public IEnumerable<ChurchResponse>? Churches { get; }
    public ApiError? Error { get; }

    public FetchChurchesResultAction(IEnumerable<ChurchResponse>? churches, ApiError? error)
    {
        Churches = churches;
        Error = error;
    }
}