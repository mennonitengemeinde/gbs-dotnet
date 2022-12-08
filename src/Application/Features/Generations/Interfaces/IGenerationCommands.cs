using Gbs.Shared.Generations;

namespace Gbs.Application.Features.Generations.Interfaces;

public interface IGenerationCommands : ICrudCommand<GenerationDto, CreateGenerationRequest, UpdateGenerationRequest> { }