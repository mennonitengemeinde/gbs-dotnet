namespace Gbs.Shared.Grades;

public class CreateGradeRequest
{
    public DateTime? Date { get; set; }
    public int Percent { get; set; }
    public int GradeTypeId { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
}

public class CreateGradeRequestValidator : AbstractValidator<CreateGradeRequest>
{
    public CreateGradeRequestValidator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.Percent).Must(x => x is > 0 and < 100).NotEmpty();
        RuleFor(x => x.GradeTypeId).NotEmpty();
        RuleFor(x => x.StudentId).NotEmpty();
        RuleFor(x => x.SubjectId).NotEmpty();
    }
}