namespace Gbs.Shared.Streams;

public class UpdateStreamRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsLive { get; set; } = false;
    public int GenerationId { get; set; }
    public IEnumerable<int> Teachers { get; set; } = new HashSet<int>();
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