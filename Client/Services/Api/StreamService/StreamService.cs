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
        if (!result.Success || result.Data == null)
        {
            _uiService.AddErrorAlert(result.Message);
            Streams = new List<LiveStream>();
            return;
        }

        Streams = result.Data;
        StreamsChanged?.Invoke();
    }

    public async Task<ServiceResponse<List<LiveStream>>> GetLiveStreams()
    {
        return await _http
            .GetAsync("api/streams")
            .EnsureSuccess<List<LiveStream>>();
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

        var streamDto = new StreamUpdateLiveDto { IsLive = !liveStream.IsLive };
        return await _http
            .PutAsJsonAsync($"api/streams/{id}/live", streamDto)
            .EnsureSuccess<LiveStream>();
    }
}