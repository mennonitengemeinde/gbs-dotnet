namespace Gbs.Shared.Churches;

public class UpdateChurchRequest : CreateChurchRequest
{
    public int Id { get; set; }
}

public class UpdateChurchRequestValidator : AbstractValidator<UpdateChurchRequest>
{
    public UpdateChurchRequestValidator()
    {
        RuleFor(x => x).SetValidator(new CreateChurchRequestValidator());
    }
}