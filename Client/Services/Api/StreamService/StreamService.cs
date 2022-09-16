namespace gbs.Client.Services.Api.StreamService;

public class StreamService : IStreamService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<LiveStream> Streams { get; set; } = new List<LiveStream>();
    public event Action? StreamsChanged;

    public StreamService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }

    public async Task LoadStreams()
    {
        var result = await GetLiveStreams();
        if (!result.Success)
        {
            _uiService.AddErrorAlert(result.Message);
            Streams = new List<LiveStream>();
            return;
        }

        Streams = result.Data;
        StreamsChanged?.Invoke();
    }

    public async Task<ServiceResponse<LiveStream>> GetOnlineLiveStreamById(int streamId)
    {
        var response = new ServiceResponse<LiveStream>();
        if (Streams.Count == 0)
        {
            await LoadStreams();
            StreamsChanged?.Invoke();
        }

        var stream = Streams.FirstOrDefault(x => x.Id == streamId && x.IsLive == true);
        if (stream == null)
        {
            response.Success = false;
            response.Message = "Stream not found";
            return response;
        }
        response.Data = stream;
        return response;
    }

    public async Task<ServiceResponse<List<LiveStream>>> GetLiveStreams()
    {
        return await _http
            .GetAsync("api/streams")
            .EnsureSuccess<List<LiveStream>>();
    }

    public async Task<ServiceResponse<LiveStream>> FetchLiveOnlyLiveStreamById(int streamId)
    {
        return await _http
            .GetAsync($"api/streams/{streamId}/live")
            .EnsureSuccess<LiveStream>();
    }

    public async Task<ServiceResponse<LiveStream>> AddLiveStream(StreamCreateDto createDto)
    {
        return await _http
            .PostAsJsonAsync("api/streams", createDto)
            .EnsureSuccess<LiveStream>();
    }

    public async Task<ServiceResponse<LiveStream>> ToggleLive(int id)
    {
        var liveStream = Streams.FirstOrDefault(s => s.Id == id);
        if (liveStream == null)
        {
            return new ServiceResponse<LiveStream>
            {
                Success = false,
                Message = "Stream not found"
            };
        }

        var streamDto = new StreamUpdateLiveDto {IsLive = !liveStream.IsLive};
        return await _http
            .PutAsJsonAsync($"api/streams/{id}/live", streamDto)
            .EnsureSuccess<LiveStream>();
    }
}