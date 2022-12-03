using Gbs.Domain.Common.Wrapper;

namespace Gbs.Application.Churches;

public class ChurchQueries : IChurchQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public ChurchQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<List<ChurchResponse>>> GetAll()
    {
        var churches = await _context.Churches
            .ProjectTo<ChurchResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
            
        return Result.Ok(churches);
    }

    public async Task<Result<ChurchResponse>> GetById(int id)
    {
        var church = await _context.Churches
            .ProjectTo<ChurchResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return church == null
            ? Result.NotFound<ChurchResponse>("Church not found")
            : Result.Ok(church);
    }
}