using Gbs.Shared.Generations;

namespace Gbs.Application.Features.Generations.Validators;

public class UpdateGenerationValidator : AbstractValidator<UpdateGenerationRequest>
{
    private readonly IGbsDbContext _context;

    public UpdateGenerationValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(g => g).SetValidator(new UpdateGenerationRequestValidator());

        RuleFor(g => g.Name)
            .MustAsync((g, name, cancellation) => BeUniqueName(g.Id, name, cancellation))
            .WithMessage("The specified name already exists.");
    }

    private async Task<bool> BeUniqueName(int id, string name, CancellationToken cancellationToken)
    {
        return await _context.Generations
            .Where(g => g.Id != id)
            .AllAsync(g => !string.Equals(g.Name, name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
}