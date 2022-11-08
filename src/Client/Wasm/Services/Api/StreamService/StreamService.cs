namespace gbs.Client.Wasm.Services.Api.StreamService;

public class StreamService : IStreamService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<StreamDto> Streams { get; set; } = new();
    public event Action? StreamsChanged;

    public StreamService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }

    public async Task CacheStreams(Result<List<StreamDto>> streamsResponse)
    {
        if (!streamsResponse.Success)
        {
            await _uiService.ShowErrorAlert(streamsResponse.Message, streamsResponse.StatusCode);
            Streams.Clear();
            return;
        }

        Streams = streamsResponse.Data;
        StreamsChanged?.Invoke();
    }

    public async Task LoadStreams()
    {
        var result = await GetLiveStreams();
        await CacheStreams(result);
    }

    public async Task<Result<List<StreamDto>>> GetLiveStreams()
    {
        return await _http
            .GetAsync("api/streams")
            .EnsureSuccess<List<StreamDto>>();
    }

    public async Task<Result<StreamDto>> GetStreamById(int streamId, bool isOnline = false)
    {
        if (Streams.Count == 0)
        {
            await LoadStreams();
            StreamsChanged?.Invoke();
        }

        var stream = isOnline
            ? Streams.FirstOrDefault(x => x.Id == streamId && x.IsLive)
            : Streams.FirstOrDefault(x => x.Id == streamId);

        return stream == null 
            ? Result.NotFound<StreamDto>("Stream not found") 
            : Result.Ok(stream);
    }

    public async Task<Result<StreamDto>> FetchStreamById(int streamId)
    {
        return await _http
            .GetAsync($"api/streams/{streamId}")
            .EnsureSuccess<StreamDto>();
    }

    public async Task<Result<StreamDto>> FetchLiveOnlyLiveStreamById(int streamId)
    {
        return await _http
            .GetAsync($"api/streams/{streamId}/live")
            .EnsureSuccess<StreamDto>();
    }

    public async Task<Result<StreamDto>> AddLiveStream(StreamCreateDto createDto)
    {
        return await _http
            .PostAsJsonAsync("api/streams", createDto)
            .EnsureSuccess<StreamDto>();
    }

    public async Task<Result<StreamDto>> UpdateStream(int streamId, StreamCreateDto createDto)
    {
        return await _http
            .PutAsJsonAsync($"api/streams/{streamId}", createDto)
            .EnsureSuccess<StreamDto>();
    }

    public async Task<Result<StreamDto>> ToggleLive(int id)
    {
        var liveStream = Streams.FirstOrDefault(s => s.Id == id);
        if (liveStream == null)
        {
            return Result.NotFound<StreamDto>("Stream not found");
        }

        var streamDto = new StreamUpdateLiveDto { IsLive = !liveStream.IsLive };
        return await _http
            .PutAsJsonAsync($"api/streams/{id}/live", streamDto)
            .EnsureSuccess<StreamDto>();
    }

    public async Task<Result<bool>> DeleteById(int id)
    {
        return await _http
            .DeleteAsync($"api/streams/{id}")
            .EnsureSuccess<bool>();
    }
}