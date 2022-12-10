namespace Gbs.Application.Features.Generations;

public class GenerationValidator : AbstractValidator<Generation>
{
    private readonly IGbsDbContext _context;

    public GenerationValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .Length(3, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.")
            .MustAsync((x, name, cancellation) =>
                BeUniqueName(x.Id, x.Name, cancellation)).WithMessage("The specified {PropertyName} already exists.");
    }

    private async Task<bool> BeUniqueName(int? id, string name, CancellationToken cancellationToken)
    {
        return await _context.Generations
            .Where(c => c.Id != id)
            .AllAsync(c => !c.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
}