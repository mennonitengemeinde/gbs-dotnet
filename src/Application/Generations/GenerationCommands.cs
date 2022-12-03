using Gbs.Application.Common.Extensions;
using Gbs.Domain.Common.Wrapper;
using Gbs.Shared.Generations;

namespace Gbs.Application.Generations;

public class GenerationCommands : IGenerationCommands
{
    private readonly IGbsDbContext _context;
    private readonly IGenerationQueries _generationQueries;

    public GenerationCommands(IGbsDbContext context, IGenerationQueries generationQueries)
    {
        _context = context;
        _generationQueries = generationQueries;
    }

    public async Task<Result<GenerationDto>> Add(GenerationCreateDto request)
    {
        if (await NameExists(request.Name))
            return Result.BadRequest<GenerationDto>("Generation name already exists");

        var newGeneration = new Generation { Name = request.Name };
        await _context.Generations.AddAsync(newGeneration);
        await _context.SaveChangesAsync();

        return await _generationQueries.GetById(newGeneration.Id);
    }

    public async Task<Result<GenerationDto>> Update(int id, GenerationUpdateDto request)
    {
        if (await NameExists(request.Name))
            return Result.BadRequest<GenerationDto>("Generation name already exists");

        var dbGeneration = await _context.Generations.FirstOrDefaultAsync(u => u.Id == id);
        if (dbGeneration == null)
            return Result.NotFound<GenerationDto>("Generation not found");

        dbGeneration.Name = request.Name;
        await _context.SaveChangesAsync();

        return await _generationQueries.GetById(dbGeneration.Id);
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var dbGeneration = await _context.Generations.FirstOrDefaultAsync(u => u.Id == id);
        if (dbGeneration == null)
            return Result.NotFound<bool>("Generation not found");

        _context.Generations.Remove(dbGeneration);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }

    private async Task<bool> NameExists(string name, int? id = null)
    {
        return id != null
            ? await _context.Generations.AnyAsync(g => g.Name == name && g.Id != id)
            : await _context.Generations.AnyAsync(g => g.Name == name);
    }
}