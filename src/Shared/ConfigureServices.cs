using System.Reflection;
using Gbs.Shared.Churches;
using Microsoft.Extensions.DependencyInjection;

namespace Gbs.Shared;

public static class ConfigureServices
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateChurchRequestValidator>();
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}