namespace Gbs.Application.Churches.Queries;

public record GetChurchQuery(int Id) : IRequest<Result<ChurchDto>>;

public class GetChurchQueryHandler : IRequestHandler<GetChurchQuery, Result<ChurchDto>>
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public GetChurchQueryHandler(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<ChurchDto>> Handle(GetChurchQuery request, CancellationToken cancellationToken)
    {
        var church = await _context.Churches
            .ProjectTo<ChurchDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            
        return church == null
            ? Result.NotFound<ChurchDto>("Church not found")
            : Result.Ok(church);
    }
}