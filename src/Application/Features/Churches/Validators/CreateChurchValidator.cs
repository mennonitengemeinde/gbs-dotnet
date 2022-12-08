namespace Gbs.Application.Features.Churches.Validators;

public class CreateChurchValidator : AbstractValidator<CreateChurchRequest>
{
    private readonly IGbsDbContext _context;

    public CreateChurchValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(c => c).SetValidator(new CreateChurchRequestValidator());

        RuleFor(c => c.Name)
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.Churches
            .AllAsync(c => !string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
}