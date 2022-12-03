using System.ComponentModel.DataAnnotations;

namespace Gbs.Shared.Churches;

public class CreateChurchRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string Country { get; set; } = string.Empty;
}

public class CreateChurchRequestValidator : AbstractValidator<CreateChurchRequest>
{
    public CreateChurchRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 150);
        RuleFor(x => x.Country).NotEmpty().Length(3, 150);
    }
}