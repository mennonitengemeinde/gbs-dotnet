namespace Gbs.Application.Features.Grades;

public class GradeValidator : AbstractValidator<Grade>
{
    private readonly IGbsDbContext _context;

    public GradeValidator(IGbsDbContext context)
    {
        _context = context;
        
        RuleFor(x => x)
            .MustAsync((x, cancellation) => BeUnique(x, cancellation)).WithMessage("Grade already exists");
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.Percent).Must(x => x is > 0 and < 100).NotEmpty();
        RuleFor(x => x.GradeTypeId).NotEmpty();
        RuleFor(x => x.StudentId).NotEmpty();
        RuleFor(x => x.SubjectId).NotEmpty();
    }
    
    private async Task<bool> BeUnique(Grade grade, CancellationToken cancellationToken)
    {
        return await _context.Grades
            .Where(g => g.Id != grade.Id)
            .AllAsync(x => x.GradeTypeId != grade.GradeTypeId 
                           && x.StudentId != grade.StudentId 
                           && x.SubjectId != grade.SubjectId, 
                cancellationToken);
    }
}