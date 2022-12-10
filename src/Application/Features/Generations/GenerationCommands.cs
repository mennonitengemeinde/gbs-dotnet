using Gbs.Application.Features.Generations.Interfaces;

namespace Gbs.Application.Features.Generations;

public class GenerationCommands : IGenerationCommands
{
    private readonly IGbsDbContext _context;
    private readonly IGenerationQueries _generationQueries;
    private readonly IValidator<Generation> _validator;

    public GenerationCommands(
        IGbsDbContext context, 
        IGenerationQueries generationQueries,
        IValidator<Generation> validator)
    {
        _context = context;
        _generationQueries = generationQueries;
        _validator = validator;
    }

    public async Task<Result<GenerationResponse>> Add(CreateGenerationRequest request)
    {
        var newGeneration = new Generation { Name = request.Name.Trim() };
        
        var resultVal = await _validator.ValidateAsync(newGeneration);
        if (!resultVal.IsValid)
            return Result.ValidationError<GenerationResponse>(resultVal);

        await _context.Generations.AddAsync(newGeneration);
        await _context.SaveChangesAsync();

        return await _generationQueries.GetById(newGeneration.Id);
    }

    public async Task<Result<GenerationResponse>> Update(UpdateGenerationRequest request)
    {
        var dbGeneration = await _context.Generations.FirstOrDefaultAsync(u => u.Id == request.Id);
        if (dbGeneration == null)
            return Result.NotFound<GenerationResponse>("Generation not found");
        
        dbGeneration.Name = request.Name.Trim();
        
        var resultVal = await _validator.ValidateAsync(dbGeneration);
        if (!resultVal.IsValid)
            return Result.ValidationError<GenerationResponse>(resultVal);

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
}