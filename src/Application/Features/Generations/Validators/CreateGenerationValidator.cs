namespace Gbs.Application.Features.Generations.Validators;

public class CreateGenerationValidator : AbstractValidator<CreateGenerationRequest>
{
    private readonly IGbsDbContext _context;

    public CreateGenerationValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(g => g).SetValidator(new CreateGenerationRequestValidator());

        RuleFor(g => g.Name)
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.Generations
            .AllAsync(g => !string.Equals(g.Name, name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
}