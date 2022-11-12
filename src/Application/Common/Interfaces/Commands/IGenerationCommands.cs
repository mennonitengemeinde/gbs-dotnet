namespace Gbs.Application.Common.Interfaces.Commands;

public interface IGenerationCommands
{
    Task<Result<GenerationDto>> Add(GenerationCreateDto request);
    Task<Result<GenerationDto>> Update(int id, GenerationUpdateDto request);
    Task<Result<bool>> Delete(int id);
}