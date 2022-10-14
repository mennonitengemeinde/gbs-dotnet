using gbs.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.HideTransitionDuration = 300;
    config.SnackbarConfiguration.ShowTransitionDuration = 300;
});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthService, AuthApiService>();
builder.Services.AddScoped<IChurchService, ChurchService>();
builder.Services.AddScoped<IUiService, UiService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenerationService, GenerationService>();
builder.Services.AddScoped<IStreamService, StreamService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(options =>
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
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization.*", LogLevel.None);

await builder.Build().RunAsync();