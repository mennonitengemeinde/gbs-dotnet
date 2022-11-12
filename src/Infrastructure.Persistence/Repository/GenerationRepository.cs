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

    public async Task<List<GenerationDto>> GetAllGenerations()
    {
        return await _context.Generations
            .ProjectTo<GenerationDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<GenerationDto?> GetGenerationById(int id)
    {
        return await _context.Generations
            .Where(g => g.Id == id)
            .ProjectTo<GenerationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<int> AddGeneration(GenerationCreateDto generation)
    {
        var newGeneration = new Generation { Name = generation.Name };
        await _context.Generations.AddAsync(newGeneration);
        await _context.SaveChangesAsync();

        return newGeneration.Id;
    }

    public async Task<Result<int>> UpdateGeneration(int generationId, GenerationUpdateDto generation)
    {
        var dbGeneration = await _context.Generations.FirstOrDefaultAsync(u => u.Id == generationId);
        if (dbGeneration == null)
            return Result.NotFound<int>("Generation not found");

        dbGeneration.Name = generation.Name;
        await _context.SaveChangesAsync();

        return Result.Ok(dbGeneration.Id);
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

    public async Task<bool> GenerationNameExists(string name, int? id = null)
    {
        return id != null
            ? await _context.Generations.AnyAsync(g => g.Name == name && g.Id != id)
            : await _context.Generations.AnyAsync(g => g.Name == name);
    }
}