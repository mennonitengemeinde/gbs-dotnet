using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gbs.Server.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            var host = configuration.GetConnectionString("Host");
            var db = configuration.GetConnectionString("Database");
            var port = configuration.GetConnectionString("Port");
            var username = configuration.GetConnectionString("Username");
            var password = configuration.GetConnectionString("Password");
            options.UseNpgsql($"Host={host};Port={port};Database={db};Username={username};Password={password}");
        });
        
        services.AddDefaultIdentity<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<DataContext>();

        return services;
    }
}