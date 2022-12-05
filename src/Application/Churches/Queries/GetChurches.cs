namespace Gbs.Application.Churches.Queries;

public record GetChurchesQuery : IRequest<Result<List<ChurchDto>>>;

public class GetChurchesQueryHandler : IRequestHandler<GetChurchesQuery, Result<List<ChurchDto>>>
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public GetChurchesQueryHandler(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<List<ChurchDto>>> Handle(GetChurchesQuery request, CancellationToken cancellationToken)
    {
        var churches = await _context.Churches
            .ProjectTo<ChurchDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
            
        return Result.Ok(churches);
    }
}