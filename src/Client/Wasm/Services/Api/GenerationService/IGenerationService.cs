namespace Gbs.Client.Wasm.Services.Api.GenerationService;

public interface IGenerationService
{
    List<GenerationDto> Generations { get; set; }
    event Action GenerationsChanged;
    Task LoadGenerations();
    Task<Result<List<GenerationDto>>> GetGenerations();
    Task<Result<GenerationDto>> GetGeneration(int id);
    Task<Result<GenerationDto>> AddGeneration(GenerationCreateDto generation);
    Task<Result<GenerationDto>> UpdateGeneration(int generationId, GenerationCreateDto generation);
    Task<Result<bool>> DeleteGeneration(int generationId);
}