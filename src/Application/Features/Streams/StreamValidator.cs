namespace Gbs.Application.Features.Streams;

public class StreamValidator : AbstractValidator<LiveStream>
{
    private readonly IGbsDbContext _context;

    public StreamValidator(IGbsDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .MustAsync((x, title, cancellation) => BeUniqueName(x.Id, title, cancellation))
            .WithMessage("A stream with the same name already exists.");

        RuleFor(x => x.Url)
            .NotEmpty()
            .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute));

        RuleFor(x => x.GenerationId)
            .GreaterThan(0);

        RuleFor(x => x.StreamTeachers)
            .NotEmpty()
            .Must(x => x.Any());
    }
    
    private async Task<bool> BeUniqueName(int? id, string title, CancellationToken cancellationToken)
    {
        return await _context.Streams
            .Where(l => l.Id != id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}