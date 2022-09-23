global using System.Net.Http.Json;
global using Microsoft.AspNetCore.Components.Authorization;
global using gbs.Client.Extensions;
global using gbs.Client.Services.UiService;
global using gbs.Client.Services.Api.AuthService;
global using gbs.Client.Services.Api.ChurchService;
global using gbs.Client.Services.Api.UserService;
global using gbs.Client.Services.Api.GenerationService;
global using gbs.Client.Services.Api.StreamService;
global using gbs.Client.Services.Api.TeacherService;
global using gbs.Shared.Dtos;
global using gbs.Shared.Dtos.Ui;
global using gbs.Shared.Enums;
global using gbs.Shared.Entities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using gbs.Client;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthService, AuthApiService>();
builder.Services.AddScoped<IChurchService, ChurchService>();
builder.Services.AddScoped<IUiService, UiService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenerationService, GenerationService>();
builder.Services.AddScoped<IStreamService, StreamService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization.*", LogLevel.None);

await builder.Build().RunAsync();
