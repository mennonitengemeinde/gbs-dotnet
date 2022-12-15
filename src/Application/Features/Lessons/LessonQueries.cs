using Gbs.Application.Common.Interfaces.Services;
using Gbs.Application.Features.Lessons.Interfaces;
using Gbs.Shared.Common.Enums;

namespace Gbs.Application.Features.Lessons;

public class LessonQueries : ILessonQueries
{
    private readonly IAuthenticatedUserService _authenticatedUserService;
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public LessonQueries(IGbsDbContext context, IMapper mapper, IAuthenticatedUserService authenticatedUserService)
    {
        _context = context;
        _mapper = mapper;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<Result<List<LessonResponse>>> GetAll(string? visibility)
    {
        if (visibility == "private" && !_authenticatedUserService.UserIsAdmin())
            return Result.Forbidden<List<LessonResponse>>();

        var lessons = visibility switch
        {
            "all" => await _context.Lessons.ProjectTo<LessonResponse>(_mapper.ConfigurationProvider)
                .OrderBy(l => l.Order)
                .ToListAsync(),
            "hidden" => await _context.Lessons.Where(l => l.IsVisible != Visibility.Private)
                .ProjectTo<LessonResponse>(_mapper.ConfigurationProvider)
                .OrderBy(l => l.Order)
                .ToListAsync(),
            _ => await _context.Lessons.Where(l => l.IsVisible == Visibility.Visible)
                .ProjectTo<LessonResponse>(_mapper.ConfigurationProvider)
                .OrderBy(l => l.Order)
                .ToListAsync()
        };

        return Result.Ok(lessons);
    }

    public async Task<Result<LessonResponse>> GetById(int id)
    {
        var lesson = await _context.Lessons
            .Where(l => l.Id == id && l.IsVisible != Visibility.Private)
            .ProjectTo<LessonResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return lesson == null
            ? Result.NotFound<LessonResponse>("Lesson not found")
            : Result.Ok(lesson);
    }
}