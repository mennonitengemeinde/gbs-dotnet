using Gbs.Application.Common.Entities;
using Gbs.Application.Common.Interfaces;
using Gbs.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gbs.Infrastructure.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IGbsDbContext, DataContext>(options =>
        {
            var host = configuration.GetConnectionString("Host");
            var db = configuration.GetConnectionString("Database");
            var port = configuration.GetConnectionString("Port");
            var username = configuration.GetConnectionString("Username");
            var password = configuration.GetConnectionString("Password");
            options.UseNpgsql($"Host={host};Port={port};Database={db};Username={username};Password={password}",
                b => b.MigrationsAssembly("Gbs.Infrastructure.Persistence"));
        });

        services.AddDefaultIdentity<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<DataContext>();

        services.AddDataProtection()
            .PersistKeysToDbContext<DataContext>();
        
        services.AddScoped<IAuthRepository, AuthRepository>();
        // services.AddScoped<IChurchRepository, ChurchRepository>();
        // services.AddScoped<IUserRepository, UserRepository>();
        // services.AddScoped<IGenerationRepository, GenerationRepository>();
        // services.AddScoped<IStreamRepository, StreamRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();

        return services;
    }
}