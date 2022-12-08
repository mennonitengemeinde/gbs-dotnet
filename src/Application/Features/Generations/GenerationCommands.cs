using Gbs.Application.Entities;
using Gbs.Application.Features.Generations.Interfaces;
using Gbs.Shared.Generations;

namespace Gbs.Application.Features.Generations;

public class GenerationCommands : IGenerationCommands
{
    private readonly IGbsDbContext _context;
    private readonly IGenerationQueries _generationQueries;
    private readonly IValidator<CreateGenerationRequest> _createGenerationValidator;
    private readonly IValidator<UpdateGenerationRequest> _updateGenerationValidator;

    public GenerationCommands(
        IGbsDbContext context, 
        IGenerationQueries generationQueries,
        IValidator<CreateGenerationRequest> createGenerationValidator,
        IValidator<UpdateGenerationRequest> updateGenerationValidator)
    {
        _context = context;
        _generationQueries = generationQueries;
        _createGenerationValidator = createGenerationValidator;
        _updateGenerationValidator = updateGenerationValidator;
    }

    public async Task<Result<GenerationDto>> Add(CreateGenerationRequest request)
    {
        var resultVal = await _createGenerationValidator.ValidateAsync(request);
        if (!resultVal.IsValid)
            return Result.ValidationError<GenerationDto>(resultVal);

        var newGeneration = new Generation { Name = request.Name };
        await _context.Generations.AddAsync(newGeneration);
        await _context.SaveChangesAsync();

        return await _generationQueries.GetById(newGeneration.Id);
    }

    public async Task<Result<GenerationDto>> Update(int id, UpdateGenerationRequest request)
    {
        var dbGeneration = await _context.Generations.FirstOrDefaultAsync(u => u.Id == id);
        if (dbGeneration == null)
            return Result.NotFound<GenerationDto>("Generation not found");
        
        var resultVal = await _updateGenerationValidator.ValidateAsync(request);
        if (!resultVal.IsValid)
            return Result.ValidationError<GenerationDto>(resultVal);

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
}