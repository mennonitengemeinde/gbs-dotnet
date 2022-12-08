namespace Gbs.Shared.Generations;

public class UpdateGenerationRequestValidator : AbstractValidator<UpdateGenerationRequest>
{
    public UpdateGenerationRequestValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}