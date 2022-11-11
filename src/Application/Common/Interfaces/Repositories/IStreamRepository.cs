namespace Gbs.Application.Common.Interfaces.Repositories;

public interface IStreamRepository
{
    Task<Result<List<StreamDto>>> GetLiveStreams();
    Task<Result<StreamDto>> GetLiveStreamById(int id, bool onlyLive = false);
    Task<Result<StreamDto>> CreateLiveStream(StreamCreateDto streamCreateDto);
    Task<Result<StreamDto>> UpdateStream(int streamId, StreamCreateDto liveDto);
    Task<Result<StreamDto>> UpdateStreamLiveStatus(int streamId, StreamUpdateLiveDto liveDto);
    Task<Result<bool>> DeleteStream(int streamId);
}