global using Microsoft.EntityFrameworkCore;
global using gbs.Server.Data;
global using gbs.Shared.Entities;
global using gbs.Shared.Const;
global using gbs.Shared.Dtos;
global using gbs.Server.Repository.AuthRepository;
global using gbs.Server.Repository.ChurchRepository;
global using gbs.Server.Repository.GenerationRepository;
global using gbs.Server.Repository.StreamRepository;
global using gbs.Server.Repository.TeacherRepository;
global using gbs.Server.Repository.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    var host = builder.Configuration.GetConnectionString("Host");
    var db = builder.Configuration.GetConnectionString("Database");
    var port = builder.Configuration.GetConnectionString("Port");
    var username = builder.Configuration.GetConnectionString("Username");
    var password = builder.Configuration.GetConnectionString("Password");
    // options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseNpgsql($"Host={host};Port={port};Database={db};Username={username};Password={password}");
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IChurchRepository, ChurchRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenerationRepository, GenerationRepository>();
builder.Services.AddScoped<IStreamRepository, StreamRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection()
    .PersistKeysToDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();