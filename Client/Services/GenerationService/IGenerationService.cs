using gbs.Shared.Dtos.Generation;

namespace gbs.Client.Services.GenerationService;

public interface IGenerationService
{
    List<Generation> Generations { get; set; }
    event Action GenerationsChanged;
    Task LoadGenerations();
    Task<List<Generation>> GetGenerations();
    Task<Generation> AddGeneration(CreateGenerationDto generation);
    Task<Generation> UpdateGeneration(int generationId, UpdateGenerationDto generation);
    Task DeleteGeneration(int generationId);
}