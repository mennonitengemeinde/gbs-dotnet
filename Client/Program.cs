global using System.Net.Http.Json;
global using Microsoft.AspNetCore.Components.Authorization;
global using gbs.Client.Services.UiService;
global using gbs.Client.Services.Api.AuthService;
global using gbs.Client.Services.Api.UserService;
global using gbs.Client.Services.Api.GenerationService;
global using gbs.Shared.Dtos;
global using gbs.Shared.Dtos.Auth;
global using gbs.Shared.Dtos.Generation;
global using gbs.Shared.Dtos.Ui;
global using gbs.Shared.Enums;
global using gbs.Shared.Entities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using gbs.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUiService, UiService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenerationService, GenerationService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
