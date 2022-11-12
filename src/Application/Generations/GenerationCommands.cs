using Gbs.Application.Common.Extensions;

namespace Gbs.Application.Generations;

public class GenerationCommands : IGenerationCommands
{
    private readonly IGenerationRepository _generationRepo;
    private readonly IGenerationQueries _generationQueries;

    public GenerationCommands(IGenerationRepository generationRepo, IGenerationQueries generationQueries)
    {
        _generationRepo = generationRepo;
        _generationQueries = generationQueries;
    }
    
    public async Task<Result<GenerationDto>> Add(GenerationCreateDto request)
    {
        if (await _generationRepo.GenerationNameExists(request.Name))
            return Result.BadRequest<GenerationDto>("Generation name already exists");
        
        var newGenId = await _generationRepo.AddGeneration(request);
        return await _generationQueries.GetById(newGenId);
    }

    public async Task<Result<GenerationDto>> Update(int id, GenerationUpdateDto request)
    {
        if (await _generationRepo.GenerationNameExists(request.Name))
            return Result.BadRequest<GenerationDto>("Generation name already exists");

        var res = await _generationRepo.UpdateGeneration(id, request);
        if (!res.Success)
            return res.Parse<int, GenerationDto>();
        
        return await _generationQueries.GetById(res.Data);
    }

    public async Task<Result<bool>> Delete(int id)
    {
        return await _generationRepo.DeleteGeneration(id);
    }
}