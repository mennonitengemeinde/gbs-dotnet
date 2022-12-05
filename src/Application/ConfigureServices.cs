using System.Reflection;
using FluentValidation;
using Gbs.Application.Churches;
using Gbs.Application.Churches.Commands;
using Gbs.Application.Common.Behaviours;
using Gbs.Application.Identity;
using Gbs.Application.Lessons;
using Gbs.Application.Students;
using Gbs.Application.Subjects;
using Gbs.Application.Teachers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Gbs.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddValidatorsFromAssemblyContaining<CreateChurchRequestValidator>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        // services.AddScoped<IChurchQueries, ChurchQueries>();
        // services.AddScoped<IChurchCommands, ChurchCommands>();
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