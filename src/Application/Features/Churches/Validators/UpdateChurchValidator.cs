namespace Gbs.Application.Features.Churches.Validators;

public class UpdateChurchValidator : AbstractValidator<UpdateChurchRequest>
{
    private readonly IGbsDbContext _context;

    public UpdateChurchValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(c => c).SetValidator(new CreateChurchRequestValidator());

        RuleFor(c => c.Name)
            .MustAsync((c, name, cancellation) => BeUniqueName(c.Id, name, cancellation))
            .WithMessage("The specified name already exists.");
    }

    private async Task<bool> BeUniqueName(int id, string name, CancellationToken cancellationToken)
    {
        return await _context.Churches
            .Where(c => c.Id != id)
            .AllAsync(c => !c.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
}