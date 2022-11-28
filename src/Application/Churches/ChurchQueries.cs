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
    
    public async Task<Result<List<ChurchDto>>> GetAll()
    {
        var churches = await _context.Churches
            .ProjectTo<ChurchDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
            
        return Result.Ok(churches);
    }

    public async Task<Result<ChurchDto>> GetById(int id)
    {
        var church = await _context.Churches
            .ProjectTo<ChurchDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return church == null
            ? Result.NotFound<ChurchDto>("Church not found")
            : Result.Ok(church);
    }
}