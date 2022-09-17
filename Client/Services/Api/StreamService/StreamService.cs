namespace gbs.Client.Services.Api.StreamService;

public class StreamService : IStreamService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<StreamGetDto> Streams { get; set; } = new List<StreamGetDto>();
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
            Streams = new List<StreamGetDto>();
            return;
        }

        Streams = result.Data;
        StreamsChanged?.Invoke();
    }

    public async Task<ServiceResponse<List<StreamGetDto>>> GetLiveStreams()
    {
        return await _http
            .GetAsync("api/streams")
            .EnsureSuccess<List<StreamGetDto>>();
    }

    public async Task<ServiceResponse<StreamGetDto>> GetStreamById(int streamId, bool isOnline = false)
    {
        var response = new ServiceResponse<StreamGetDto>();
        if (Streams.Count == 0)
        {
            await LoadStreams();
            StreamsChanged?.Invoke();
        }

        var stream = isOnline
            ? Streams.FirstOrDefault(x => x.Id == streamId && x.IsLive)
            : Streams.FirstOrDefault(x => x.Id == streamId);

        if (stream == null)
        {
            response.Success = false;
            response.Message = "Stream not found";
            return response;
        }

        response.Data = stream;
        return response;
    }

    public async Task<ServiceResponse<StreamGetDto>> FetchStreamById(int streamId)
    {
        return await _http
            .GetAsync($"api/streams/{streamId}")
            .EnsureSuccess<StreamGetDto>();
    }

    public async Task<ServiceResponse<StreamGetDto>> FetchLiveOnlyLiveStreamById(int streamId)
    {
        return await _http
            .GetAsync($"api/streams/{streamId}/live")
            .EnsureSuccess<StreamGetDto>();
    }

    public async Task<ServiceResponse<StreamGetDto>> AddLiveStream(StreamCreateDto createDto)
    {
        return await _http
            .PostAsJsonAsync("api/streams", createDto)
            .EnsureSuccess<StreamGetDto>();
    }

    public async Task<ServiceResponse<StreamGetDto>> UpdateStream(int streamId, StreamCreateDto createDto)
    {
        return await _http
            .PutAsJsonAsync($"api/streams/{streamId}", createDto)
            .EnsureSuccess<StreamGetDto>();
    }

    public async Task<ServiceResponse<StreamGetDto>> ToggleLive(int id)
    {
        var liveStream = Streams.FirstOrDefault(s => s.Id == id);
        if (liveStream == null)
        {
            return new ServiceResponse<StreamGetDto>
            {
                Success = false,
                Message = "Stream not found"
            };
        }

        var streamDto = new StreamUpdateLiveDto {IsLive = !liveStream.IsLive};
        return await _http
            .PutAsJsonAsync($"api/streams/{id}/live", streamDto)
            .EnsureSuccess<StreamGetDto>();
    }

    public async Task<ServiceResponse<bool>> DeleteById(int id)
    {
        return await _http
            .DeleteAsync($"api/streams/{id}")
            .EnsureSuccess<bool>();
    }
}