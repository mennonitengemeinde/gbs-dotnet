namespace Gbs.Application.Features.Lessons.Validators;

public class CreateLessonValidator : AbstractValidator<CreateLessonRequest>
{
    private readonly IGbsDbContext _context;

    public CreateLessonValidator(IGbsDbContext context)
    {
        _context = context;
        
        RuleFor(x => x).SetValidator(new CreateLessonRequestValidator());
        
        RuleFor(x => x.Name)
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }
    
    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.Lessons
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}