using Gbs.Shared.Streams;

namespace Gbs.Wasm.Services.Api;

public class StreamService : BaseApiCrud<StreamResponse, CreateStreamRequest, CreateStreamRequest, int>, IStreamService
{
    public StreamService(IDateTimeService dateTimeService, IUiService uiService, HttpClient http) : base(
        dateTimeService, uiService, http) { }

    public override string BaseUrl => "api/streams";
    
    public async Task<StreamResponse?> GetById(int id)
    {
        await Fetch();
        return Data.FirstOrDefault(c => c.Id == id);
    }

    public async Task<StreamResponse?> GetOnlyLiveById(int id)
    {
        await Fetch();
        return Data.FirstOrDefault(x => x.Id == id && x.IsLive);
    }

    public async Task ToggleLive(int id)
    {
        SetLoading(true);
        var liveStream = Data.FirstOrDefault(s => s.Id == id);
        if (liveStream == null)
            return;

        var streamDto = new UpdateStreamLiveRequest { IsLive = !liveStream.IsLive };
        var result = await Http
            .PutAsJsonAsync($"api/streams/{id}/live", streamDto)
            .EnsureSuccess<StreamResponse>();

        if (!result.Success)
            await SetError(new ServiceError(result.Message, result.Errors, result.StatusCode));

        await ForceFetch();
    }
}