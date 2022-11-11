using AutoMapper.QueryableExtensions;

namespace Gbs.Infrastructure.Persistence.Repository;

public class GenerationRepository : IGenerationRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GenerationRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<GenerationDto>>> GetAllGenerations()
    {
        var generations = await _context.Generations
            .ProjectTo<GenerationDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Result.Ok(generations);
    }

    public async Task<Result<GenerationDto>> GetGenerationById(int id)
    {
        var generation = await _context.Generations
            .Where(g => g.Id == id)
            .ProjectTo<GenerationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return generation == null
            ? Result.NotFound<GenerationDto>("Generation not found")
            : Result.Ok(generation);
    }

    public async Task<Result<GenerationDto>> AddGeneration(GenerationCreateDto generation)
    {
        if (await GenerationNameExists(generation.Name))
        {
            return Result.BadRequest<GenerationDto>("Generation already exists.");
        }

        var newGeneration = new Generation { Name = generation.Name };
        await _context.Generations.AddAsync(newGeneration);
        await _context.SaveChangesAsync();

        return await GetGenerationById(newGeneration.Id);
    }

    public async Task<Result<GenerationDto>> UpdateGeneration(int generationId, GenerationUpdateDto generation)
    {
        if (await GenerationNameExists(generation.Name, generationId))
        {
            return Result.BadRequest<GenerationDto>("Generation already exists.");
        }

        var dbGeneration = await _context.Generations.FirstOrDefaultAsync(u => u.Id == generationId);
        if (dbGeneration == null)
            return Result.NotFound<GenerationDto>("Generation not found");

        dbGeneration.Name = generation.Name;
        await _context.SaveChangesAsync();

        return await GetGenerationById(dbGeneration.Id);
    }

    public async Task<Result<bool>> DeleteGeneration(int id)
    {
        var dbGeneration = await _context.Generations.FirstOrDefaultAsync(u => u.Id == id);
        if (dbGeneration == null)
            return Result.NotFound<bool>("Generation not found");

        _context.Generations.Remove(dbGeneration);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }

    private async Task<bool> GenerationNameExists(string name, int? id = null)
    {
        return id != null
            ? await _context.Generations.AnyAsync(g => g.Name == name && g.Id != id)
            : await _context.Generations.AnyAsync(g => g.Name == name);
    }
}