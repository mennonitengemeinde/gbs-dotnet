namespace Gbs.Application.Features.Lessons.Validators;

public class UpdateLessonValidator : AbstractValidator<UpdateLessonRequest>
{
    private readonly IGbsDbContext _context;

    public UpdateLessonValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(x => x).SetValidator(new UpdateLessonRequestValidator());

        RuleFor(x => x.Name)
            .MustAsync((x, name, cancellation) => BeUniqueName(x.Id, name, cancellation))
            .WithMessage("The specified name already exists.");
    }

    private async Task<bool> BeUniqueName(int id, string name, CancellationToken cancellationToken)
    {
        return await _context.Lessons
            .Where(l => l.Id != id)
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}