using Gbs.Application.Features.Streams.Interfaces;

namespace Gbs.Application.Features.Streams;

public class StreamCommands : IStreamCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<LiveStream> _validator;

    public StreamCommands(IGbsDbContext context, IMapper mapper, IValidator<LiveStream> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<Result<StreamResponse>> CreateStream(CreateStreamRequest streamCreateDto)
    {
        var stream = _mapper.Map<LiveStream>(streamCreateDto);

        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == streamCreateDto.GenerationId);
        if (generation == null)
            return Result.NotFound<StreamResponse>("Generation not found");

        stream.StreamTeachers.Clear();

        var streamTeachers = streamCreateDto.Teachers
            .Select(teacherId => new LiveStreamTeacher { TeacherId = teacherId, LiveStreamId = stream.Id })
            .ToList();
        stream.StreamTeachers.AddRange(streamTeachers);
        
        var resultVal = await _validator.ValidateAsync(stream);
        if (!resultVal.IsValid)
            return Result.ValidationError<StreamResponse>(resultVal);
        
        _context.Streams.Add(stream);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<StreamResponse>(stream));
    }

    public async Task<Result<StreamResponse>> UpdateStream(int id, CreateStreamRequest streamCreateDto)
    {
        var stream = await _context.Streams
            .Include(s => s.StreamTeachers)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (stream == null)
            return Result.NotFound<StreamResponse>("Stream not found");

        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == streamCreateDto.GenerationId);
        if (generation == null)
            return Result.NotFound<StreamResponse>("Generation not found");

        foreach (var streamTeacher in stream.StreamTeachers
                     .Where(streamTeacher => !streamCreateDto.Teachers.Contains(streamTeacher.TeacherId)))
        {
            _context.StreamTeachers.Remove(streamTeacher);
        }

        foreach (var teacherId in streamCreateDto.Teachers
                     .Where(teacherId => stream.StreamTeachers.All(st => st.TeacherId != teacherId)))
        {
            stream.StreamTeachers.Add(new LiveStreamTeacher { TeacherId = teacherId, LiveStreamId = stream.Id });
        }

        stream.Title = streamCreateDto.Title;
        stream.Url = streamCreateDto.Url;
        stream.IsLive = streamCreateDto.IsLive;
        stream.Generation = generation;
        
        var resultVal = await _validator.ValidateAsync(stream);
        if (!resultVal.IsValid)
            return Result.ValidationError<StreamResponse>(resultVal);
        
        _context.Streams.Update(stream);
        await _context.SaveChangesAsync();
        
        return Result.Ok(_mapper.Map<StreamResponse>(stream));   
    }
    
    public async Task<Result<bool>> DeleteStream(int streamId)
    {
        var stream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (stream == null)
            return Result.BadRequest<bool>("Stream not found");
        _context.Streams.Remove(stream);
        await _context.SaveChangesAsync();
        return Result.Ok(true, "Stream deleted successfully");
    }
    
    public async Task<Result<StreamResponse>> UpdateLiveStatus(int streamId, UpdateStreamLiveRequest liveDto)
    {
        var dbStream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (dbStream == null)
            return Result.NotFound<StreamResponse>("Stream not found");

        dbStream.IsLive = liveDto.IsLive;
        await _context.SaveChangesAsync();
        
        return Result.Ok(_mapper.Map<StreamResponse>(dbStream));
    }
}