namespace Gbs.Shared.Streams;

public class UpdateStreamRequest : CreateStreamRequest, IStreamRequest
{
    public int Id { get; set; }
}

public class UpdateStreamRequestValidator : AbstractValidator<UpdateStreamRequest>
{
    public UpdateStreamRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Url)
            .NotEmpty()
            .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute));

        RuleFor(x => x.GenerationId)
            .GreaterThan(0);

        RuleFor(x => x.Teachers)
            .NotEmpty()
            .Must(x => x.Any());
    }
}