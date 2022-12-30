namespace Gbs.Application.Features.Generations;

public class GenerationMapping : Profile
{
    public GenerationMapping()
    {
        CreateMap<Generation, GenerationResponse>();
    }
}