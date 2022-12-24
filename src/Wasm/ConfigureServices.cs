using Blazored.LocalStorage;
using Gbs.Wasm.Services.Api;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

namespace Gbs.Wasm;

public static class ConfigureServices
{
    public static IServiceCollection AddWasmServices(this IServiceCollection services,
        IWebAssemblyHostEnvironment hostEnvironment)
    {
        services.AddFluxor(options =>
        {
            options.ScanAssemblies(typeof(Program).Assembly);
#if DEBUG
            options.UseReduxDevTools();
#endif
        });
        services.AddBlazoredLocalStorage();
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.HideTransitionDuration = 300;
            config.SnackbarConfiguration.ShowTransitionDuration = 300;
        });

        services.AddTransient<IDateTimeService, DateTimeService>();

        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(hostEnvironment.BaseAddress) });
        services.AddScoped<IUiService, UiService>();
        services.AddScoped<IAuthService, AuthApiService>();
        services.AddScoped<ChurchService>();

        services.AddOptions();
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy(Policies.RequireAdmins,
                policy => policy.RequireClaim("role", Roles.SuperAdmin, Roles.Admin));
            options.AddPolicy(Policies.RequireAdminsAndSound,
                policy => policy.RequireClaim("role", Roles.SuperAdmin, Roles.Admin, Roles.Sound));
            options.AddPolicy(Policies.RequireAdminsAndTeachers,
                policy => policy.RequireClaim("role", Roles.SuperAdmin, Roles.Admin, Roles.Teacher, Roles.ChurchLeader,
                    Roles.ChurchTeacher));
            options.AddPolicy(Policies.RequireAdminsSoundAndTeachers,
                policy => policy.RequireClaim("role", Roles.SuperAdmin, Roles.Admin, Roles.Sound, Roles.Teacher,
                    Roles.ChurchLeader, Roles.ChurchTeacher));
        });
        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

        return services;
    }
}