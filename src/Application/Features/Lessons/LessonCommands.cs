using Gbs.Application.Common.Interfaces.Services;
using Gbs.Application.Features.Lessons.Interfaces;

namespace Gbs.Application.Features.Lessons;

public class LessonCommands : ILessonCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<Lesson> _validator;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public LessonCommands(
        IGbsDbContext context, 
        IMapper mapper,
        IValidator<Lesson> validator,
        IAuthenticatedUserService authenticatedUserService)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<Result<LessonResponse>> Add(CreateLessonRequest request)
    {
        var lesson = _mapper.Map<Lesson>(request);
        
        var resultVal = await _validator.ValidateAsync(lesson);
        if (!resultVal.IsValid)
            return Result.ValidationError<LessonResponse>(resultVal);

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

        lesson.Order = lastOrder + 1;
        await _context.Lessons.AddAsync(lesson);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<LessonResponse>(lesson));
    }

    public async Task<Result<LessonResponse>> Update(int id, CreateLessonRequest request)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return Result.NotFound<LessonResponse>("Lesson not found");

        _mapper.Map(request, lesson);
        
        var resultVal = await _validator.ValidateAsync(lesson);
        if (!resultVal.IsValid)
            return Result.ValidationError<LessonResponse>(resultVal);

        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<LessonResponse>(lesson));
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

    public async Task<Result<LessonResponse>> UpdateOrder(int id, int request)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return Result.NotFound<LessonResponse>("Lesson not found");

        if (lesson.Order == request)
            return Result.Ok(_mapper.Map<LessonResponse>(lesson));

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
        return Result.Ok(_mapper.Map<LessonResponse>(lesson));
    }

    public async Task<Result<LessonResponse>> UpdateWatched(int id, bool request)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return Result.NotFound<LessonResponse>("Lesson not found");

        var watched = await _context.LessonsWatched.FirstOrDefaultAsync(x => x.LessonId == id && x.UserId == _authenticatedUserService.GetUserId());
        if (watched == null)
        {
            if (request == false)
                return Result.Ok(_mapper.Map<LessonResponse>(lesson));
            
            watched = new LessonWatched
            {
                LessonId = id,
                UserId = _authenticatedUserService.GetUserId(),
                WatchedAt = DateTime.UtcNow
            };
            await _context.LessonsWatched.AddAsync(watched);
        }
        else
        {
            if (request == false)
            {
                _context.LessonsWatched.Remove(watched);
            }
            else
            {
                watched.WatchedAt = DateTime.UtcNow;
                _context.LessonsWatched.Update(watched);
            }
        }
        
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<LessonResponse>(lesson));
    }
}