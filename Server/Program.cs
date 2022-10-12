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

builder.Services.AddDefaultIdentity<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DataContext>();

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
        policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Teacher));
    options.AddPolicy(Policies.RequireAdminsSoundAndTeachers,
        policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Sound, Roles.Teacher));
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