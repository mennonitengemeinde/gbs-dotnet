

namespace gbs.Server.Repository.StreamRepository;

public interface IStreamRepository
{
    Task<ServiceResponse<List<StreamGetDto>>> GetLiveStreams();
    Task<ServiceResponse<StreamGetDto>> GetLiveStreamById(int id, bool onlyLive = false);
    Task<ServiceResponse<StreamGetDto>> CreateLiveStream(StreamCreateDto streamCreateDto);
    Task<ServiceResponse<StreamGetDto>> UpdateStream(int streamId, StreamCreateDto liveDto);
    Task<ServiceResponse<StreamGetDto>> UpdateStreamLiveStatus(int streamId, StreamUpdateLiveDto liveDto);
    Task<ServiceResponse<bool>> DeleteStream(int streamId);
}