namespace gbs.Client.Services.Api.GenerationService;

public interface IGenerationService
{
    List<Generation> Generations { get; set; }
    event Action GenerationsChanged;
    Task LoadGenerations();
    Task<ServiceResponse<List<Generation>>> GetGenerations();
    Task<ServiceResponse<Generation>> GetGeneration(int Id);
    Task<ServiceResponse<Generation>> AddGeneration(GenerationCreateDto generation);
    Task<ServiceResponse<Generation>> UpdateGeneration(int generationId, GenerationCreateDto generation);
    Task<ServiceResponse<bool>> DeleteGeneration(int generationId);
}