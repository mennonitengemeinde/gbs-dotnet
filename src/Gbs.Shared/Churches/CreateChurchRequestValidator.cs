namespace Gbs.Shared.Churches;

public class CreateChurchRequestValidator : AbstractValidator<CreateChurchRequest>
{
    public CreateChurchRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 150);
        RuleFor(x => x.Country).NotEmpty().Length(3, 150);
    }
}