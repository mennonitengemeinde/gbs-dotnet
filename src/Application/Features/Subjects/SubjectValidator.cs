namespace Gbs.Application.Features.Subjects;

public class SubjectValidator : AbstractValidator<Subject>
{
    private readonly IGbsDbContext _context;

    public SubjectValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Subject name is required")
            .MinimumLength(3).WithMessage("Subject name must be at least 3 characters long")
            .MustAsync((x, name, cancellation) => BeUniqueName(x.Id, name, cancellation))
            .WithMessage("Subject name must be unique");
    }

    private async Task<bool> BeUniqueName(int? id, string name, CancellationToken cancellationToken)
    {
        return await _context.Subjects
            .Where(l => l.Id != id)
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}