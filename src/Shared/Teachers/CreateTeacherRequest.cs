namespace Gbs.Shared.Teachers;

public class CreateTeacherRequest
{
    public string Name { get; set; } = string.Empty;
}

public class CreateTeacherRequestValidator : AbstractValidator<CreateTeacherRequest>
{
    public CreateTeacherRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long");
    }
}