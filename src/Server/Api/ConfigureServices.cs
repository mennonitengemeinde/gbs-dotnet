using gbs.Server.Api.Services;
using Gbs.Server.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;

namespace gbs.Server.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSignalR();
        services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" });
        });

        services.AddControllersWithViews();
        services.AddRazorPages();

        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // ValidIssuer = Configuration["JwtIssuer"],
                    // ValidAudience = Configuration["JwtAudience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Secret").Value))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.RequireAdmins,
                policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin));
            options.AddPolicy(Policies.RequireAdminsAndSound,
                policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Sound));
            options.AddPolicy(Policies.RequireAdminsAndTeachers,
                policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Teacher, Roles.ChurchLeader,
                    Roles.ChurchTeacher));
            options.AddPolicy(Policies.RequireAdminsSoundAndTeachers,
                policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Sound, Roles.Teacher,
                    Roles.ChurchLeader, Roles.ChurchTeacher));
        });

        services.AddHttpContextAccessor();

        return services;
    }
}