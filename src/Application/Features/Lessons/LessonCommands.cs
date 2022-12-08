using Gbs.Application.Features.Lessons.Interfaces;

namespace Gbs.Application.Features.Lessons;

public class LessonCommands : ILessonCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateLessonRequest> _createLessonValidator;
    private readonly IValidator<UpdateLessonRequest> _updateLessonValidator;

    public LessonCommands(
        IGbsDbContext context, 
        IMapper mapper,
        IValidator<CreateLessonRequest> createLessonValidator,
        IValidator<UpdateLessonRequest> updateLessonValidator)
    {
        _context = context;
        _mapper = mapper;
        _createLessonValidator = createLessonValidator;
        _updateLessonValidator = updateLessonValidator;
    }

    public async Task<Result<LessonResponse>> Add(CreateLessonRequest request)
    {
        var resultVal = await _createLessonValidator.ValidateAsync(request);
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

        var lesson = _mapper.Map<Lesson>(request);
        lesson.Order = lastOrder + 1;
        await _context.Lessons.AddAsync(lesson);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<LessonResponse>(lesson));
    }

    public async Task<Result<LessonResponse>> Update(UpdateLessonRequest request)
    {
        var lesson = await _context.Lessons.FindAsync(request.Id);
        if (lesson == null)
            return Result.NotFound<LessonResponse>("Lesson not found");
        
        var resultVal = await _updateLessonValidator.ValidateAsync(request);
        if (!resultVal.IsValid)
            return Result.ValidationError<LessonResponse>(resultVal);

        lesson.Name = request.Name;
        lesson.SubjectId = request.SubjectId;
        lesson.GenerationId = request.GenerationId;
        lesson.IsVisible = request.IsVisible;
        lesson.VideoUrl = request.VideoUrl;

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
}