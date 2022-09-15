namespace gbs.Server.Repository.GenerationRepository;

public interface IGenerationRepository
{
    Task<ServiceResponse<List<Generation>>> GetAllGenerations();
    Task<ServiceResponse<Generation>> GetGenerationById(int id);
    Task<ServiceResponse<Generation>> AddGeneration(GenerationCreateDto generation);
    Task<ServiceResponse<Generation>> UpdateGeneration(int generationId, GenerationUpdateDto generation);
    Task<ServiceResponse<bool>> DeleteGeneration(int id);
}