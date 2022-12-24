using Gbs.Wasm.Common.Models;
using Gbs.Wasm.Services.Api;
using Gbs.Wasm.Store.ChurchUseCase.Actions;

namespace Gbs.Wasm.Store.ChurchUseCase;

public class Effects
{
    private readonly ChurchService _churchService;

    public Effects(ChurchService churchService)
    {
        _churchService = churchService;
    }

    [EffectMethod]
    public async Task HandleFetchChurchesAction(FetchChurchesAction action, IDispatcher dispatcher)
    {
        var result = await _churchService.GetAll();
        if (result.Success == false || result.Data == null)
        {
            dispatcher.Dispatch(new FetchChurchesResultAction(
                result.Data,
                new ApiError { Message = result.Message, Errors = result.Errors, StatusCode = result.StatusCode }));
        }
        else
        {
            dispatcher.Dispatch(new FetchChurchesResultAction(result.Data, null));
        }
    }
}