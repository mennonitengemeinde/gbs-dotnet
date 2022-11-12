using Gbs.Application.Common.Extensions;

namespace Gbs.Application.Streams;

public class StreamCommands : IStreamCommands
{
    private readonly IStreamRepository _streamRepo;
    private readonly IGenerationRepository _generationRepo;
    private readonly IStreamQueries _streamQueries;

    public StreamCommands(IStreamRepository streamRepo, IGenerationRepository generationRepo, IStreamQueries streamQueries)
    {
        _streamRepo = streamRepo;
        _generationRepo = generationRepo;
        _streamQueries = streamQueries;
    }
    
    public async Task<Result<StreamDto>> CreateStream(StreamCreateDto streamCreateDto)
    {
        if (await _streamRepo.StreamWithTitleExists(streamCreateDto.Title))
            return Result.BadRequest<StreamDto>("Stream with this title already exists");

        var newStreamId = await _streamRepo.CreateLiveStream(streamCreateDto);
        if (!newStreamId.Success)
            return newStreamId.Parse<int, StreamDto>();
        
        return await _streamQueries.GetStreamById(newStreamId.Data);
    }

    public async Task<Result<StreamDto>> UpdateStream(int streamId, StreamCreateDto streamCreateDto)
    {
        if (await _streamRepo.StreamWithTitleExists(streamCreateDto.Title))
            return Result.BadRequest<StreamDto>("Stream with this title already exists");
        
        var updatedStreamId = await _streamRepo.UpdateStream(streamId, streamCreateDto);
        if (!updatedStreamId.Success)
            return updatedStreamId.Parse<bool, StreamDto>();
        
        return await _streamQueries.GetStreamById(streamId);   
    }

    public async Task<Result<bool>> DeleteStream(int streamId)
    {
        return await _streamRepo.DeleteStream(streamId);
    }

    public async Task<Result<StreamDto>> UpdateLiveStatus(int streamId, StreamUpdateLiveDto liveDto)
    {
        var updatedStreamId = await _streamRepo.UpdateStreamLiveStatus(streamId, liveDto);
        if (!updatedStreamId.Success)
            return updatedStreamId.Parse<bool, StreamDto>();
        
        return await _streamQueries.GetStreamById(streamId);
    }
}