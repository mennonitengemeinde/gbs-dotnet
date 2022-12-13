using Gbs.Application.Features.Generations.Interfaces;

namespace Gbs.Application.Features.Generations;

public class GenerationQueries : IGenerationQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public GenerationQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<GenerationResponse>>> GetAll()
    {
        var response = await _context.Generations
            .ProjectTo<GenerationResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Result.Ok(response);
    }

    public async Task<Result<GenerationResponse>> GetById(int id)
    {
        var response = await _context.Generations
            .Where(g => g.Id == id)
            .ProjectTo<GenerationResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return response == null
            ? Result.NotFound<GenerationResponse>("Generation not found")
            : Result.Ok(response);
    }
}