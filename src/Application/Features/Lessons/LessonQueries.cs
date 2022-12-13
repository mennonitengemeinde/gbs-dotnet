using Gbs.Application.Features.Lessons.Interfaces;

namespace Gbs.Application.Features.Lessons;

public class LessonQueries : ILessonQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public LessonQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<LessonResponse>>> GetAll()
    {
        var lessons = await _context.Lessons
            .ProjectTo<LessonResponse>(_mapper.ConfigurationProvider)
            .OrderBy(l => l.Order)
            .ToListAsync();
        return Result.Ok(lessons);
    }

    public async Task<Result<LessonResponse>> GetById(int id)
    {
        var lesson = await _context.Lessons
            .ProjectTo<LessonResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return lesson == null
            ? Result.NotFound<LessonResponse>("Lesson not found")
            : Result.Ok(lesson);
    }
}