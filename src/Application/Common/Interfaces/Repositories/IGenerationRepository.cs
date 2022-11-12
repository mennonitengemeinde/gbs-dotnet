namespace Gbs.Application.Common.Interfaces.Repositories;

public interface IGenerationRepository
{
    Task<List<GenerationDto>> GetAllGenerations();
    Task<GenerationDto?> GetGenerationById(int id);
    Task<int> AddGeneration(GenerationCreateDto generation);
    Task<Result<int>> UpdateGeneration(int generationId, GenerationUpdateDto generation);
    Task<Result<bool>> DeleteGeneration(int id);
    Task<bool> GenerationNameExists(string name, int? id = null);
}