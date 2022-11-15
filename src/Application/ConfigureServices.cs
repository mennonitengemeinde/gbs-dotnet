using System.Reflection;
using Gbs.Application.Subjects;
using Microsoft.Extensions.DependencyInjection;

namespace Gbs.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IGenerationQueries, GenerationQueries>();
        services.AddScoped<IGenerationCommands, GenerationCommands>();
        services.AddScoped<ISubjectQueries, SubjectQueries>();
        services.AddScoped<ISubjectCommands, SubjectCommands>();
        services.AddScoped<IStreamQueries, StreamQueries>();
        services.AddScoped<IStreamCommands, StreamCommands>();


        return services;
    }
}