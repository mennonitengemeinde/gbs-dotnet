using System.Reflection;
using Gbs.Application.Features.Churches;
using Gbs.Application.Features.Churches.Interfaces;
using Gbs.Application.Features.Churches.Validators;
using Gbs.Application.Features.Generations;
using Gbs.Application.Features.Generations.Interfaces;
using Gbs.Application.Features.Identity;
using Gbs.Application.Features.Identity.Interfaces;
using Gbs.Application.Features.Lessons;
using Gbs.Application.Features.Lessons.Interfaces;
using Gbs.Application.Features.Streams;
using Gbs.Application.Features.Streams.Interfaces;
using Gbs.Application.Features.Students;
using Gbs.Application.Features.Students.Interfaces;
using Gbs.Application.Features.Subjects;
using Gbs.Application.Features.Subjects.Interfaces;
using Gbs.Application.Features.Teachers;
using Gbs.Application.Features.Teachers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Gbs.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddValidatorsFromAssemblyContaining<CreateChurchValidator>();
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // services.AddMediatR(Assembly.GetExecutingAssembly());
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        services.AddScoped<IChurchQueries, ChurchQueries>();
        services.AddScoped<IChurchCommands, ChurchCommands>();
        services.AddScoped<IGenerationQueries, GenerationQueries>();
        services.AddScoped<IGenerationCommands, GenerationCommands>();
        services.AddScoped<IIdentityQueries, IdentityQueries>();
        services.AddScoped<IIdentityCommands, IdentityCommands>();
        services.AddScoped<ILessonQueries, LessonQueries>();
        services.AddScoped<ILessonCommands, LessonCommands>();
        services.AddScoped<ISubjectQueries, SubjectQueries>();
        services.AddScoped<ISubjectCommands, SubjectCommands>();
        services.AddScoped<IStreamQueries, StreamQueries>();
        services.AddScoped<IStreamCommands, StreamCommands>();
        services.AddScoped<IStudentQueries, StudentQueries>();
        services.AddScoped<IStudentCommands, StudentCommands>();
        services.AddScoped<ITeacherQueries, TeacherQueries>();
        services.AddScoped<ITeacherCommands, TeacherCommands>();

        return services;
    }
}