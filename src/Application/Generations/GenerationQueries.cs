using Gbs.Domain.Common.Wrapper;

namespace Gbs.Application.Generations;

public class GenerationQueries : IGenerationQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public GenerationQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<GenerationDto>>> GetAll()
    {
        var response = await _context.Generations
            .ProjectTo<GenerationDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Result.Ok(response);
    }

    public async Task<Result<GenerationDto>> GetById(int id)
    {
        var response = await _context.Generations
            .Where(g => g.Id == id)
            .ProjectTo<GenerationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return response == null
            ? Result.NotFound<GenerationDto>("Generation not found")
            : Result.Ok(response);
    }
}