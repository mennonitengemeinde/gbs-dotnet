namespace Gbs.Application.Features.Teachers;

public class TeacherValidator : AbstractValidator<Teacher>
{
    private readonly IGbsDbContext _context;

    public TeacherValidator(IGbsDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
            .MustAsync((x, name,cancellation) => BeUniqueName(x.Id, x.Name, cancellation)).WithMessage("The specified name already exists.");
    }
    
    private async Task<bool> BeUniqueName(int? id, string name, CancellationToken cancellationToken)
    {
        return await _context.Teachers
            .Where(c => c.Id != id)
            .AllAsync(c => !c.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
}