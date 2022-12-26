using Gbs.Wasm.Common.Models;
using Gbs.Wasm.Services.Api;

namespace Gbs.Wasm.Store.ChurchUseCase;

public class ChurchEffects
{
    private readonly ChurchService _churchService;

    public ChurchEffects(ChurchService churchService)
    {
        _churchService = churchService;
    }

    [EffectMethod]
    public async Task HandleFetchChurchesAction(FetchChurchesAction action, IDispatcher dispatcher)
    {
        var result = await _churchService.GetAll();
        if (result.Success == false || result.Data == null)
        {
            dispatcher.Dispatch(new FetchChurchesActionResult(
                result.Data,
                new ApiError { Message = result.Message, Errors = result.Errors, StatusCode = result.StatusCode }));
        }
        else
        {
            dispatcher.Dispatch(new FetchChurchesActionResult(result.Data, null));
        }
    }
}