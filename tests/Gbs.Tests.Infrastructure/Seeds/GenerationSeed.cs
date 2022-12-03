using Gbs.Domain.Entities;

namespace Gbs.Tests.Infrastructure.Seeds;

public static class GenerationSeed
{
    public static List<Generation> GetGenerations()
    {
        return new List<Generation>
        {
            new Generation { Id = 1, Name = "Generation 1" },
            new Generation { Id = 2, Name = "Generation 2" },
            new Generation { Id = 3, Name = "Generation 3" }
        };
    }
}