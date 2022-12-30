namespace Gbs.Application.Features.GradeTypes;

public class GradeTypeValidator : AbstractValidator<GradeType>
{
    private readonly IGbsDbContext _context;

    public GradeTypeValidator(IGbsDbContext context)
    {
        _context = context;
        RuleFor(x => x.Name)
            .NotEmpty()
            .MustAsync((x, name, cancellation) =>
                BeUniqueName(x.Id, x.Name, cancellation)).WithMessage("The specified {PropertyName} already exists.");
        
        RuleFor(x => x.GenerationId).NotEmpty();
    }
    
    private async Task<bool> BeUniqueName(int? id, string name, CancellationToken cancellationToken)
    {
        return await _context.GradeTypes
            .Where(c => c.Id != id)
            .AllAsync(c => !c.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
}