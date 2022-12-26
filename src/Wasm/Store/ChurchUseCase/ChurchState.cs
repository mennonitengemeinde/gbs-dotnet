using Gbs.Wasm.Common.Models;

namespace Gbs.Wasm.Store.ChurchUseCase;

[FeatureState]
public record ChurchState(bool IsLoading, IEnumerable<ChurchResponse>? Churches, ApiError? Error)
{
    public static readonly ChurchState Empty = new(false, null, null);

    private ChurchState() : this(IsLoading: false, Churches: Array.Empty<ChurchResponse>(), null) { }
}