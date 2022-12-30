namespace Gbs.Shared.GradeTypes;

public class CreateGradeTypeRequest
{
    public string Name { get; set; } = string.Empty;
    public int GenerationId { get; set; }
}

public class CreateGradeTypeRequestValidator : AbstractValidator<CreateGradeTypeRequest>
{
    public CreateGradeTypeRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.GenerationId).NotEmpty();
    }
}