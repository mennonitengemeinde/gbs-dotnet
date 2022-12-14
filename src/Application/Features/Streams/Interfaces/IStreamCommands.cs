namespace Gbs.Application.Features.Streams.Interfaces;

public interface IStreamCommands
{
    Task<Result<StreamResponse>> CreateStream(CreateStreamRequest streamCreateDto);
    Task<Result<StreamResponse>> UpdateStream(int id, CreateStreamRequest streamCreateDto);
    Task<Result<bool>> DeleteStream(int streamId);
    Task<Result<StreamResponse>> UpdateLiveStatus(int streamId, UpdateStreamLiveRequest liveDto);
}