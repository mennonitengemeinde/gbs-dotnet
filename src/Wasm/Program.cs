using Gbs.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Gbs.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddFluentValidationServices();
builder.Services.AddWasmServices(builder.HostEnvironment);

builder.Logging.AddFilter("Microsoft.AspNetCore.Authorization.*", LogLevel.None);

await builder.Build().RunAsync();