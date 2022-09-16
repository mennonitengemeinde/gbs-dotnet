namespace gbs.Client.Services.Api.StreamService;

public interface IStreamService
{
    List<LiveStream> Streams { get; set; }
    event Action StreamsChanged;
    Task LoadStreams();
    Task<ServiceResponse<LiveStream>> GetOnlineLiveStreamById(int streamId);
    Task<ServiceResponse<List<LiveStream>>> GetLiveStreams();
    Task<ServiceResponse<LiveStream>> FetchLiveOnlyLiveStreamById(int streamId);
    Task<ServiceResponse<LiveStream>> AddLiveStream(StreamCreateDto createDto);
    Task<ServiceResponse<LiveStream>> ToggleLive(int id);
}