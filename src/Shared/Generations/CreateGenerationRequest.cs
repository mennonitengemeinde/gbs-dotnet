namespace Gbs.Shared.Generations;

public class CreateGenerationRequest
{
    public string Name { get; set; } = string.Empty;
}

public class CreateGenerationRequestValidator : AbstractValidator<CreateGenerationRequest>
{
    public CreateGenerationRequestValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}