namespace Gbs.Application.Lessons;

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
        if (await NameExists(request.Name, request.SubjectId, request.GenerationId))
            return Result.BadRequest<LessonDto>("Lesson name already exists");

        var lesson = _mapper.Map<Lesson>(request);
        await _context.Lessons.AddAsync(lesson);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<LessonDto>(lesson));
    }

    public async Task<Result<LessonDto>> Update(int id, LessonCreateDto request)
    {
        if (await NameExists(request.Name, request.SubjectId, request.GenerationId))
            return Result.BadRequest<LessonDto>("Lesson name already exists");

        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return Result.NotFound<LessonDto>("Lesson not found");
        
        lesson.Name = request.Name;
        lesson.SubjectId = request.SubjectId;
        lesson.GenerationId = request.GenerationId;
        lesson.Order = request.Order;
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

    private async Task<bool> NameExists(string name, int subjectId, int generationId, int? id = null)
    {
        return id == null
            ? await _context.Lessons
                .AnyAsync(x =>
                    x.Name == name
                    && x.SubjectId == subjectId
                    && x.GenerationId == generationId)
            : await _context.Lessons
                .AnyAsync(x =>
                    x.Name == name
                    && x.SubjectId == subjectId
                    && x.GenerationId == generationId
                    && x.Id != id);
    }
}