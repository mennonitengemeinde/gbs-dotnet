using System.Reflection;
using Gbs.Application.Churches;
using Gbs.Application.Lessons;
using Gbs.Application.Subjects;
using Gbs.Application.Teachers;
using Gbs.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Gbs.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IChurchQueries, ChurchQueries>();
        services.AddScoped<IChurchCommands, ChurchCommands>();
        services.AddScoped<IGenerationQueries, GenerationQueries>();
        services.AddScoped<IGenerationCommands, GenerationCommands>();
        services.AddScoped<ILessonQueries, LessonQueries>();
        services.AddScoped<ILessonCommands, LessonCommands>();
        services.AddScoped<ISubjectQueries, SubjectQueries>();
        services.AddScoped<ISubjectCommands, SubjectCommands>();
        services.AddScoped<IStreamQueries, StreamQueries>();
        services.AddScoped<IStreamCommands, StreamCommands>();
        services.AddScoped<ITeacherQueries, TeacherQueries>();
        services.AddScoped<ITeacherCommands, TeacherCommands>();
        services.AddScoped<IUserQueries, UserQueries>();
        services.AddScoped<IUserCommands, UserCommands>();

        return services;
    }
}