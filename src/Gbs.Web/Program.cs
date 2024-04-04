using Gbs.Application;
using Gbs.Application.Common.Interfaces.Services;
using Gbs.Application.Entities;
using Gbs.Infrastructure.Persistence;
using Gbs.Shared.Common.Const;
using Gbs.Web.Components;
using Gbs.Web.Components.Account;
using Gbs.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Policies.RequireAdmins, policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin))
    .AddPolicy(Policies.RequireAdminsAndSound, policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Sound))
    .AddPolicy(Policies.RequireAdminsAndTeachers, policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin,
        Roles.Teacher, Roles.ChurchLeader,
        Roles.ChurchTeacher))
    .AddPolicy(Policies.RequireAdminsSoundAndTeachers, policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin,
        Roles.Sound, Roles.Teacher,
        Roles.ChurchLeader, Roles.ChurchTeacher));

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
//                        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddSignInManager()
//     .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();