using Gbs.Domain.Common.Wrapper;
using Gbs.Shared.Lessons;

namespace Gbs.Application.Lessons;

public class LessonQueries : ILessonQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public LessonQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<LessonDto>>> GetAll()
    {
        var lessons = await _context.Lessons
            .ProjectTo<LessonDto>(_mapper.ConfigurationProvider)
            .OrderBy(l => l.Order)
            .ToListAsync();
        return Result.Ok(lessons);
    }

    public async Task<Result<LessonDto>> GetById(int id)
    {
        var lesson = await _context.Lessons
            .ProjectTo<LessonDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return lesson == null
            ? Result.NotFound<LessonDto>("Lesson not found")
            : Result.Ok(lesson);
    }
}