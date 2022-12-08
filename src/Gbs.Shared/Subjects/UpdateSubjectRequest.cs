using System.ComponentModel.DataAnnotations;

namespace Gbs.Shared.Subjects;

public class UpdateSubjectRequest
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateSubjectRequestValidator : AbstractValidator<UpdateSubjectRequest>
{
    public UpdateSubjectRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Subject name is required")
            .MinimumLength(3).WithMessage("Subject name must be at least 3 characters long");
    }
}