

namespace gbs.Server.Repository.StreamRepository;

public interface IStreamRepository
{
    Task<ServiceResponse<List<LiveStream>>> GetLiveStreams();
    Task<ServiceResponse<LiveStream>> CreateLiveStream(StreamCreateDto streamCreateDto);
    Task<ServiceResponse<LiveStream>> UpdateStreamLiveStatus(int streamId, StreamUpdateLiveDto liveDto);
}