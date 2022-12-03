using Gbs.Application.Common.Extensions;
using Gbs.Domain.Common.Wrapper;
using Gbs.Shared.Streams;

namespace Gbs.Application.Streams;

public class StreamCommands : IStreamCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public StreamCommands(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<StreamDto>> CreateStream(StreamCreateDto streamCreateDto)
    {
        if (await TitleExists(streamCreateDto.Title))
            return Result.BadRequest<StreamDto>("Stream with this title already exists");
        
        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == streamCreateDto.GenerationId);
        if (generation == null)
            return Result.NotFound<StreamDto>("Generation not found");

        var stream = _mapper.Map<LiveStream>(streamCreateDto);

        stream.StreamTeachers.Clear();

        var streamTeachers = streamCreateDto.Teachers
            .Select(teacherId => new LiveStreamTeacher { TeacherId = teacherId, LiveStreamId = stream.Id })
            .ToList();
        stream.StreamTeachers.AddRange(streamTeachers);
        _context.Streams.Add(stream);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<StreamDto>(stream));
    }

    public async Task<Result<StreamDto>> UpdateStream(int streamId, StreamCreateDto streamCreateDto)
    {
        if (await TitleExists(streamCreateDto.Title, streamId))
            return Result.BadRequest<StreamDto>("Stream with this title already exists");
        
        var stream = await _context.Streams
            .Include(s => s.StreamTeachers)
            .FirstOrDefaultAsync(s => s.Id == streamId);

        if (stream == null)
            return Result.NotFound<StreamDto>("Stream not found");

        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == streamCreateDto.GenerationId);
        if (generation == null)
            return Result.NotFound<StreamDto>("Generation not found");

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
        _context.Streams.Update(stream);
        await _context.SaveChangesAsync();
        
        return Result.Ok(_mapper.Map<StreamDto>(stream));   
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
    
    public async Task<Result<StreamDto>> UpdateLiveStatus(int streamId, StreamUpdateLiveDto liveDto)
    {
        var dbStream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (dbStream == null)
            return Result.NotFound<StreamDto>("Stream not found");

        dbStream.IsLive = liveDto.IsLive;
        await _context.SaveChangesAsync();
        
        return Result.Ok(_mapper.Map<StreamDto>(dbStream));
    }
    
    private async Task<bool> TitleExists(string title, int? id = null)
    {
        return id == null
            ? await _context.Streams.AnyAsync(s => s.Title.ToLower().Equals(title.ToLower()))
            : await _context.Streams.AnyAsync(s => s.Id != id && s.Title.ToLower().Equals(title.ToLower()));
    }
}