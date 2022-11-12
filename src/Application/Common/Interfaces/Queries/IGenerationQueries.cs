namespace Gbs.Application.Common.Interfaces.Queries;

public interface IGenerationQueries
{
    Task<Result<List<GenerationDto>>> GetAll();
    Task<Result<GenerationDto>> GetById(int id);
}