namespace Gbs.Application.Churches.Commands;

public record DeleteChurchCommand(int Id) : IRequest<Result<bool>>;

public class DeleteChurchCommandHandler : IRequestHandler<DeleteChurchCommand, Result<bool>>
{
    private readonly IGbsDbContext _context;

    public DeleteChurchCommandHandler(IGbsDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result<bool>> Handle(DeleteChurchCommand request, CancellationToken cancellationToken)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (dbChurch == null)
            return Result.NotFound<bool>("Church not found");

        _context.Churches.Remove(dbChurch);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok(true);
    }
}