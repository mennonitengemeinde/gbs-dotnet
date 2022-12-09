using Gbs.Shared.Streams;

namespace Gbs.Wasm.Store;

public class StreamStore : BaseStore<StreamResponse, int, CreateStreamRequest, UpdateStreamRequest>, IStreamStore
{
    public StreamStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public override string BaseUrl { get; } = "api/streams";
    
    public override StreamResponse? GetByIdQuery(int id) => Data.FirstOrDefault(x => x.Id == id);
    
    public async Task<StreamResponse?> GetOnlyLiveById(int id)
    {
        await Fetch();

        var result = Data.FirstOrDefault(x => x.Id == id && x.IsLive);
        if (result == null)
        {
            UiService.ShowErrorAlert("Could not find item with id " + id);
        }

        return result;
    }

    public async Task ToggleLive(int id)
    {
        var liveStream = Data.FirstOrDefault(s => s.Id == id);
        if (liveStream == null)
        {
            UiService.ShowErrorAlert("Could not find item with id " + id);
            return;
        }
        
        var streamDto = new UpdateStreamLiveRequest {IsLive = !liveStream.IsLive};
        var result = await Http
            .PutAsJsonAsync($"api/streams/{id}/live", streamDto)
            .EnsureSuccess<StreamResponse>();
        
        if (!result.Success)
        {
            UiService.ShowErrorAlert(result.Message, result.StatusCode);
            SetErrors(result.Message, result.Errors);
            return;
        }

        await ForceFetch();
    }
}