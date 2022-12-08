using System.ComponentModel.DataAnnotations;

namespace Gbs.Shared.Subjects;

public class CreateSubjectRequest
{
    public string Name { get; set; } = string.Empty;
}

public class CreateSubjectRequestValidator : AbstractValidator<CreateSubjectRequest>
{
    public CreateSubjectRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Subject name is required")
            .MinimumLength(3).WithMessage("Subject name must be at least 3 characters long");
    }
}