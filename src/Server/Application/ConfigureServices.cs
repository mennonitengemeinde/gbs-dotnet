using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Gbs.Server.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}