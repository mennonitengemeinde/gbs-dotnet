namespace gbs.Server.Repository.GenerationRepository;

public interface IGenerationRepository
{
    Task<ServiceResponse<List<Generation>>> GetAllGenerations();
    Task<ServiceResponse<Generation>> GetGenerationById(int id);
    Task<ServiceResponse<Generation>> AddGeneration(CreateGenerationDto generation);
    Task<ServiceResponse<Generation>> UpdateGeneration(int generationId, UpdateGenerationDto generation);
    Task<ServiceResponse<bool>> DeleteGeneration(int id);
}