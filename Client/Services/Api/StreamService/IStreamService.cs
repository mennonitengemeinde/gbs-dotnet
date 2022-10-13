namespace gbs.Client.Services.Api.StreamService;

public interface IStreamService
{
    List<StreamGetDto> Streams { get; set; }
    event Action StreamsChanged;
    Task CacheStreams(ServiceResponse<List<StreamGetDto>> streamsResponse);
    Task LoadStreams();
    Task<ServiceResponse<List<StreamGetDto>>> GetLiveStreams();
    Task<ServiceResponse<StreamGetDto>> GetStreamById(int streamId, bool isOnline = false);
    Task<ServiceResponse<StreamGetDto>> FetchStreamById(int streamId);
    Task<ServiceResponse<StreamGetDto>> FetchLiveOnlyLiveStreamById(int streamId);
    Task<ServiceResponse<StreamGetDto>> AddLiveStream(StreamCreateDto createDto);
    Task<ServiceResponse<StreamGetDto>> UpdateStream(int streamId, StreamCreateDto createDto);
    Task<ServiceResponse<StreamGetDto>> ToggleLive(int id);
    Task<ServiceResponse<bool>> DeleteById(int id);
}