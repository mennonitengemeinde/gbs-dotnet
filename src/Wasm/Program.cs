using Gbs.Shared;
using Gbs.Wasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddFluentValidationServices();
builder.Services.AddWasmServices(builder.HostEnvironment);

builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization.*", LogLevel.None);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
await builder.Build().RunAsync();