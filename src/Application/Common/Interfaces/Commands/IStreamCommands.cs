using Gbs.Domain.Common.Wrapper;
using Gbs.Shared.Streams;

namespace Gbs.Application.Common.Interfaces.Commands;

public interface IStreamCommands
{
    Task<Result<StreamDto>> CreateStream(StreamCreateDto streamCreateDto);
    Task<Result<StreamDto>> UpdateStream(int streamId, StreamCreateDto streamCreateDto);
    Task<Result<bool>> DeleteStream(int streamId);
    Task<Result<StreamDto>> UpdateLiveStatus(int streamId, StreamUpdateLiveDto liveDto);
}