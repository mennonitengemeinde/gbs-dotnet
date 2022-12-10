namespace Gbs.Shared.Generations;

public class UpdateGenerationRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

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