using System.Reflection;
using Blazored.LocalStorage;
using Gbs.Wasm.Services.Api.AuthService;
using Gbs.Wasm.Services.Api.ChurchService;
using Gbs.Wasm.Services.Api.GenerationService;
using Gbs.Wasm.Services.Api.StreamService;
using Gbs.Wasm.Services.Api.StudentService;
using Gbs.Wasm.Services.Api.TeacherService;
using Gbs.Wasm.Services.Api.UserService;
using Gbs.Wasm.Store;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

namespace Gbs.Wasm;

public static class ConfigureServices
{
    public static IServiceCollection AddWasmServices(this IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
    {
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
        services.AddScoped<IStore<GenerationDto, GenerationCreateDto>, GenerationStore>();
        services.AddScoped<IAuthService, AuthApiService>();
        services.AddScoped<IChurchService, ChurchService>();
        services.AddScoped<IUiService, UiService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGenerationService, GenerationService>();
        services.AddScoped<IStreamService, StreamService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        
        services.AddOptions();
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy(Policies.RequireAdmins,
                policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin));
            options.AddPolicy(Policies.RequireAdminsAndSound,
                policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Sound));
            options.AddPolicy(Policies.RequireAdminsAndTeachers,
                policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Teacher, Roles.ChurchLeader, Roles.ChurchTeacher));
            options.AddPolicy(Policies.RequireAdminsSoundAndTeachers,
                policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Sound, Roles.Teacher, Roles.ChurchLeader, Roles.ChurchTeacher));
        });
        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        
        return services;
    }
}