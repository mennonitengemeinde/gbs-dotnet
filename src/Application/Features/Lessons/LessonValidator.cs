namespace Gbs.Application.Features.Lessons;

public class LessonValidator : AbstractValidator<Lesson>
{
    private readonly IGbsDbContext _context;

    public LessonValidator(IGbsDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
            .MustAsync((x, name, cancellation) => BeUniqueName(x.Id, name, cancellation))
            .WithMessage("The specified name already exists.");;
        
        RuleFor(x => x.IsVisible)
            .NotEmpty();
        
        RuleFor(x => x.GenerationId)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(x => x.TeacherId)
            .NotEmpty()
            .GreaterThan(0);
    }
    
    private async Task<bool> BeUniqueName(int? id, string name, CancellationToken cancellationToken)
    {
        return await _context.Lessons
            .Where(l => l.Id != id)
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}