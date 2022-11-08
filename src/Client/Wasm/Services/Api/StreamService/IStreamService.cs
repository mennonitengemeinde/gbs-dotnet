namespace gbs.Client.Wasm.Services.Api.StreamService;

public interface IStreamService
{
    List<StreamDto> Streams { get; set; }
    event Action StreamsChanged;
    Task CacheStreams(Result<List<StreamDto>> streamsResponse);
    Task LoadStreams();
    Task<Result<List<StreamDto>>> GetLiveStreams();
    Task<Result<StreamDto>> GetStreamById(int streamId, bool isOnline = false);
    Task<Result<StreamDto>> FetchStreamById(int streamId);
    Task<Result<StreamDto>> FetchLiveOnlyLiveStreamById(int streamId);
    Task<Result<StreamDto>> AddLiveStream(StreamCreateDto createDto);
    Task<Result<StreamDto>> UpdateStream(int streamId, StreamCreateDto createDto);
    Task<Result<StreamDto>> ToggleLive(int id);
    Task<Result<bool>> DeleteById(int id);
}