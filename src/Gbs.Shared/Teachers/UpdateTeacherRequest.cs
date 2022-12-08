namespace Gbs.Shared.Teachers;

public class UpdateTeacherRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UpdateTeacherRequestValidator : AbstractValidator<UpdateTeacherRequest>
{
    public UpdateTeacherRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long");
    }
}