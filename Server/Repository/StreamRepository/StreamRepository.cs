﻿namespace gbs.Server.Repository.StreamRepository;

public class StreamRepository : IStreamRepository
{
    private readonly DataContext _context;
    private readonly IHubContext<LivestreamHub> _streamHub;

    public StreamRepository(DataContext context, IHubContext<LivestreamHub> streamHub)
    {
        _context = context;
        _streamHub = streamHub;
    }
    
    private async Task NotifyClients()
    {
        var streams = await GetLiveStreams();
        await _streamHub.Clients.All.SendAsync("ReceiveStreams", streams);
    }

    public async Task<ServiceResponse<List<StreamGetDto>>> GetLiveStreams()
    {
        var result = await _context.Streams
            .Select(s => new StreamGetDto
            {
                Id = s.Id,
                Title = s.Title,
                Url = s.Url,
                GenerationId = s.GenerationId,
                IsLive = s.IsLive,
                Generation = s.Generation,
                Teachers = s.StreamTeachers.Select(st => st.Teacher).ToList()
            })
            .ToListAsync();
        return new ServiceResponse<List<StreamGetDto>> { Data = result };
    }

    public async Task<ServiceResponse<StreamGetDto>> GetLiveStreamById(int id, bool onlyLive = false)
    {
        var liveStream = await _context.Streams
            .Where(s => s.Id == id)
            .Select(s => new StreamGetDto
            {
                Id = s.Id,
                Title = s.Title,
                Url = s.Url,
                GenerationId = s.GenerationId,
                IsLive = s.IsLive,
                Generation = s.Generation,
                Teachers = s.StreamTeachers.Select(st => st.Teacher).ToList()
            })
            .FirstOrDefaultAsync();
        return liveStream == null || (onlyLive && !liveStream.IsLive)
            ? ServiceResponse<StreamGetDto>.BadRequest("Stream not found")
            : new ServiceResponse<StreamGetDto> { Data = liveStream };
    }

    public async Task<ServiceResponse<StreamGetDto>> CreateLiveStream(StreamCreateDto streamCreateDto)
    {
        var titleIsValid = await TitleIsValid<StreamGetDto>(streamCreateDto.Title);
        if (!titleIsValid.Success)
            return titleIsValid;

        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == streamCreateDto.GenerationId);
        if (generation == null)
            return ServiceResponse<StreamGetDto>.BadRequest("Generation not found");

        var stream = new LiveStream
        {
            Title = streamCreateDto.Title,
            Url = streamCreateDto.Url,
            IsLive = streamCreateDto.IsLive,
            Generation = generation
        };

        stream.StreamTeachers.Clear();

        var streamTeachers = streamCreateDto.Teachers
            .Select(teacherId => new LiveStreamTeacher { TeacherId = teacherId, LiveStreamId = stream.Id })
            .ToList();
        stream.StreamTeachers.AddRange(streamTeachers);
        _context.Streams.Add(stream);
        await _context.SaveChangesAsync();
        await NotifyClients();
        return await GetLiveStreamById(stream.Id);
    }

    public async Task<ServiceResponse<StreamGetDto>> UpdateStream(int streamId, StreamCreateDto liveDto)
    {
        var stream = await _context.Streams
            .Include(s => s.StreamTeachers)
            .FirstOrDefaultAsync(s => s.Id == streamId);

        if (stream == null)
            return ServiceResponse<StreamGetDto>.BadRequest("Stream not found");

        var titleIsValid = await TitleIsValid<StreamGetDto>(liveDto.Title, streamId);
        if (!titleIsValid.Success)
            return titleIsValid;

        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == liveDto.GenerationId);
        if (generation == null)
            return ServiceResponse<StreamGetDto>.BadRequest("Generation not found");

        foreach (var streamTeacher in stream.StreamTeachers
                     .Where(streamTeacher => !liveDto.Teachers.Contains(streamTeacher.TeacherId)))
        {
            _context.StreamTeachers.Remove(streamTeacher);
        }

        foreach (var teacherId in liveDto.Teachers
                     .Where(teacherId => stream.StreamTeachers.All(st => st.TeacherId != teacherId)))
        {
            stream.StreamTeachers.Add(new LiveStreamTeacher { TeacherId = teacherId, LiveStreamId = stream.Id });
        }

        stream.Title = liveDto.Title;
        stream.Url = liveDto.Url;
        stream.IsLive = liveDto.IsLive;
        stream.Generation = generation;
        _context.Streams.Update(stream);
        await _context.SaveChangesAsync();
        await NotifyClients();
        return await GetLiveStreamById(stream.Id);
    }

    public async Task<ServiceResponse<StreamGetDto>> UpdateStreamLiveStatus(int streamId, StreamUpdateLiveDto liveDto)
    {
        var dbStream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (dbStream == null)
        {
            return ServiceResponse<StreamGetDto>.BadRequest("Stream not found");
        }

        dbStream.IsLive = liveDto.IsLive;
        await _context.SaveChangesAsync();
        await NotifyClients();
        return await GetLiveStreamById(dbStream.Id);
    }

    public async Task<ServiceResponse<bool>> DeleteStream(int streamId)
    {
        var stream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (stream == null)
            return ServiceResponse<bool>.BadRequest("Stream not found");
        _context.Streams.Remove(stream);
        await _context.SaveChangesAsync();
        await NotifyClients();
        return new ServiceResponse<bool> { Data = true, Message = "Stream deleted." };
    }

    private async Task<ServiceResponse<T>> TitleIsValid<T>(string title, int? streamId = null)
    {
        if (streamId == null)
        {
            if (await _context.Streams.AnyAsync(s => s.Title.ToLower().Equals(title.ToLower())))
                return ServiceResponse<T>.BadRequest("Stream not found");
        }
        else
        {
            if (await _context.Streams.AnyAsync(s => s.Title.ToLower().Equals(title.ToLower()) && s.Id != streamId))
                return ServiceResponse<T>.BadRequest("Title already exists");
        }

        return new ServiceResponse<T>();
    }
}