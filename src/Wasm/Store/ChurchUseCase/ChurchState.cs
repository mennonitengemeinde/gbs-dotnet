using Gbs.Wasm.Common.Models;

namespace Gbs.Wasm.Store.ChurchUseCase;

[FeatureState]
public class ChurchState
{
    public IEnumerable<ChurchResponse> Churches { get; }
    public bool IsLoading { get; }
    public ApiError? Error { get; }

    private ChurchState() { }
    
    public ChurchState(bool isLoading, IEnumerable<ChurchResponse>? churches, ApiError? error)
    {
        IsLoading = isLoading;
        Churches = churches ?? Array.Empty<ChurchResponse>();
        Error = error;
    }
}