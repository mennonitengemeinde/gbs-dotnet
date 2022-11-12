namespace Gbs.Application.Generations;

public class GenerationQueries : IGenerationQueries
{
    private readonly IGenerationRepository _generationRepo;

    public GenerationQueries(IGenerationRepository generationRepo)
    {
        _generationRepo = generationRepo;
    }

    public async Task<Result<List<GenerationDto>>> GetAll()
    {
        var response = await _generationRepo.GetAllGenerations();
        return Result.Ok(response);
    }

    public async Task<Result<GenerationDto>> GetById(int id)
    {
        var response = await _generationRepo.GetGenerationById(id);
        return response == null
            ? Result.NotFound<GenerationDto>("Generation not found")
            : Result.Ok(response);
    }
}