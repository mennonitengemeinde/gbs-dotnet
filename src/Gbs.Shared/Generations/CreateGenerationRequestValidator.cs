namespace Gbs.Shared.Generations;

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