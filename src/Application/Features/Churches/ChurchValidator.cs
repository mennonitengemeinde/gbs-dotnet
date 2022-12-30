namespace Gbs.Application.Features.Churches;

public class ChurchValidator : AbstractValidator<Church>
{
    private readonly IGbsDbContext _context;

    public ChurchValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 150)
            .MustAsync((x, name,cancellation) => BeUniqueName(x.Id, x.Name, cancellation)).WithMessage("The specified name already exists.");
        
        RuleFor(x => x.Country)
            .NotEmpty()
            .Length(3, 150);
    }
    
    private async Task<bool> BeUniqueName(int? id, string name, CancellationToken cancellationToken)
    {
        return await _context.Churches
            .Where(c => c.Id != id)
            .AllAsync(c => !c.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
}