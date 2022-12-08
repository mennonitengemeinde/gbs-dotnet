using Gbs.Application.Entities;
using Gbs.Shared.Lessons;

namespace Gbs.Application.Features.Lessons;

public class LessonCommands : ILessonCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public LessonCommands(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<LessonDto>> Add(LessonCreateDto request)
    {
        if (await NameExists(request.Name, request.GenerationId, request.SubjectId))
            return Result.BadRequest<LessonDto>("Lesson name already exists");

        int lastOrder;

        try
        {
            lastOrder = await _context.Lessons
                .Where(l => l.GenerationId == request.GenerationId)
                .MaxAsync(l => l.Order);
        }
        catch (InvalidOperationException)
        {
            lastOrder = 0;
        }

        var lesson = _mapper.Map<Lesson>(request);
        lesson.Order = lastOrder + 1;
        await _context.Lessons.AddAsync(lesson);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<LessonDto>(lesson));
    }

    public async Task<Result<LessonDto>> Update(int id, LessonCreateDto request)
    {
        if (await NameExists(request.Name, request.GenerationId, request.SubjectId, id))
            return Result.BadRequest<LessonDto>("Lesson name already exists");

        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return Result.NotFound<LessonDto>("Lesson not found");

        lesson.Name = request.Name;
        lesson.SubjectId = request.SubjectId;
        lesson.GenerationId = request.GenerationId;
        lesson.IsVisible = request.IsVisible;
        lesson.VideoUrl = request.VideoUrl;

        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<LessonDto>(lesson));
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return Result.NotFound<bool>("Lesson not found");

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    public async Task<Result<LessonDto>> UpdateOrder(int id, int request)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return Result.NotFound<LessonDto>("Lesson not found");

        if (lesson.Order == request)
            return Result.Ok(_mapper.Map<LessonDto>(lesson));

        var conflictLessons = await _context.Lessons
            .Where(l => l.GenerationId == lesson.GenerationId && l.Order == request)
            .FirstOrDefaultAsync();
        if (conflictLessons != null)
        {
            conflictLessons.Order = lesson.Order;
            _context.Lessons.Update(conflictLessons);
        }

        lesson.Order = request;
        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<LessonDto>(lesson));
    }

    private async Task<bool> NameExists(string name, int generationId, int? subjectId, int? id = null)
    {
        if (subjectId == null)
        {
            if (id == null)
            {
                return await _context.Lessons
                    .AnyAsync(x =>
                        x.Name == name
                        && x.GenerationId == generationId);
            }

            return await _context.Lessons
                .AnyAsync(x =>
                    x.Name == name
                    && x.Id != id
                    && x.GenerationId == generationId);
        }

        if (id == null)
        {
            return await _context.Lessons
                .AnyAsync(x =>
                    x.Name == name
                    && x.SubjectId == subjectId
                    && x.GenerationId == generationId);
        }

        return await _context.Lessons
            .AnyAsync(x =>
                x.Name == name
                && x.SubjectId == subjectId
                && x.Id != id
                && x.GenerationId == generationId);
    }
}