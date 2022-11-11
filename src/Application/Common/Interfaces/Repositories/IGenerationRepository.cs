namespace Gbs.Application.Common.Interfaces.Repositories;

public interface IGenerationRepository
{
    Task<Result<List<GenerationDto>>> GetAllGenerations();
    Task<Result<GenerationDto>> GetGenerationById(int id);
    Task<Result<GenerationDto>> AddGeneration(GenerationCreateDto generation);
    Task<Result<GenerationDto>> UpdateGeneration(int generationId, GenerationUpdateDto generation);
    Task<Result<bool>> DeleteGeneration(int id);
}