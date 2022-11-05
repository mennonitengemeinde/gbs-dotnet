using Gbs.Core.Domain.Const;
using Gbs.Server.Application.Common.Interfaces.Repositories;
using Gbs.Server.Persistence;
using Gbs.Server.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IChurchRepository, ChurchRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenerationRepository, GenerationRepository>();
builder.Services.AddScoped<IStreamRepository, StreamRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                    System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value))
        };
    });
builder.Services.AddAuthorization(options =>
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

builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection()
    .PersistKeysToDbContext<DataContext>();

var app = builder.Build();

app.UseResponseCompression();

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
app.MapHub<LivestreamHub>("/hubs/streamshub");
app.MapFallbackToFile("index.html");

app.Run();