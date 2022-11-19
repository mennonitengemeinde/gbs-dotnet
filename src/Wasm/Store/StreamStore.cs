namespace Gbs.Wasm.Store;

public class StreamStore : BaseStore<StreamDto, StreamCreateDto, StreamCreateDto>, IStreamStore
{
    public StreamStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public override string BaseUrl { get; } = "api/streams";
    
    public override StreamDto? GetByIdQuery(int id) => Value.FirstOrDefault(x => x.Id == id);
    
    public async Task<StreamDto?> GetOnlyLiveById(int id)
    {
        await Initialize();

        var result = Value.FirstOrDefault(x => x.Id == id && x.IsLive);
        if (result == null)
        {
            await UiService.ShowErrorAlert("Could not find item with id " + id);
        }

        return result;
    }

    public async Task ToggleLive(int id)
    {
        var liveStream = Value.FirstOrDefault(s => s.Id == id);
        if (liveStream == null)
        {
            await UiService.ShowErrorAlert("Could not find item with id " + id);
            return;
        }
        
        var streamDto = new StreamUpdateLiveDto {IsLive = !liveStream.IsLive};
        var result = await Http
            .PutAsJsonAsync($"api/streams/{id}/live", streamDto)
            .EnsureSuccess<StreamDto>();
        
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            return;
        }

        await Fetch();
    }
}