namespace Gbs.Application.Common.Interfaces.Repositories;

public interface IStreamRepository
{
    Task<List<StreamDto>> GetLiveStreams();
    Task<StreamDto?> GetLiveStreamById(int id, bool onlyLive = false);
    Task<Result<int>> CreateLiveStream(StreamCreateDto streamCreateDto);
    Task<Result<bool>> UpdateStream(int streamId, StreamCreateDto liveDto);
    Task<Result<bool>> UpdateStreamLiveStatus(int streamId, StreamUpdateLiveDto liveDto);
    Task<Result<bool>> DeleteStream(int streamId);
    Task<bool> StreamWithTitleExists(string title);
}