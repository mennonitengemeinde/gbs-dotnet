using gbs.Shared.Dtos.Generation;

namespace gbs.Client.Services.Api.GenerationService;

public interface IGenerationService
{
    List<Generation> Generations { get; set; }
    event Action GenerationsChanged;
    Task LoadGenerations();
    Task<ServiceResponse<List<Generation>>> GetGenerations();
    Task<ServiceResponse<Generation>> AddGeneration(CreateGenerationDto generation);
    Task<ServiceResponse<Generation>> UpdateGeneration(int generationId, UpdateGenerationDto generation);
    Task<ServiceResponse<bool>> DeleteGeneration(int generationId);
}