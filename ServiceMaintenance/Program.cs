using ServiceMaintenance.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Services;
using ServiceMaintenance.ObjectModel;
using DevExpress.XtraCharts;
using DevExpress.Blazor.Reporting;

using ServiceMaintenance.Models;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Microsoft.AspNetCore.Components.Authorization;

using ServiceMaintenance.Seeds;
using Microsoft.AspNetCore.Authorization;
using ServiceMaintenance.Filters;

using ServiceMaintenance.Contants;
using Radzen;
using ServiceMaintenance.Filters.Security;
using ServiceMaintenance.Services.ApisConfiguration;
using ServiceMaintenance.Filters.GlobalSecurity;
using ServiceMaintenance.Areas.Identity;

using ServiceMaintenance.Chat;

using Microsoft.AspNetCore.Http.Features;
using ServiceMaintenance.Pages.Parents.ItemModule;




var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ServiceMaintenanceContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ServiceMaintenanceContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddDbContext<ServiceMaintenanceContext>(ServiceLifetime.Transient);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDevExpressBlazor();
builder.Services.AddDevExpressBlazorReporting();
builder.Services.AddMvc();
builder.Services.AddScoped<switcher.ThemeSwitcher.ThemeService>();
builder.Services.AddDevExpressServerSideBlazorReportViewer();
builder.Services.AddSyncfusionBlazor();

builder.Services.AddAuthentication("Identity.Application").AddCookie();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddSignalR();



builder.Services.AddDevExpressBlazor(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});
builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();

SyncfusionLicenseProvider.RegisterLicense("NjA1NkAzMjM2MkUzMTJFMzliSzVTQlJKN0NLVzNVOFVKSlErcVEzYW9PSkZ2dUhicHliVjkrMncxdHpRPQ==\r\n");
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();


// Add HTTP clients
// Apply HTTP client and form options configurations
ServiceConfigurations.ConfigureHttpClients(builder.Services, "https://localhost:44313/");
ServiceConfigurations.ConfigureFormOptions(builder.Services);


builder.Services.AddHttpClient<ItemService>(client =>
{
    client.BaseAddress = new Uri("https://bis.com.kh/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, certificate, chain, errors) => true
});

builder.Services.AddHttpClient<RepairItemService>(client =>
{
    client.BaseAddress = new Uri("https://bis.com.kh/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, certificate, chain, errors) => true
});

builder.Services.AddAuthorization(options =>
{
    AuthorizationPolicies.AddPolicies(options);
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 2L * 1024 * 1024 * 1024; // 2 GB
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2L * 1024 * 1024 * 1024; // 2 GB
});


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(ServiceReportMapper));


// Register custom authorization services
builder.Services.AddSingleton<IAuthorizationHandler, ModuleAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddSingleton<EmojiService>(); // Register EmojiService as a singleton
builder.Services.AddScoped<RolesService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PermissionService>();
builder.Services.AddScoped<StateManagementAction>();
builder.Services.AddAuthorization(); // Ensure authorization services are registered


builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    // Set to zero to re-validate user tokens immediately after any changes
    options.ValidationInterval = TimeSpan.Zero;
});



var app = builder.Build();

// Seed data
await app.Services.SeedDataAsync();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<ChatHub>("/chatHub");
app.MapHub<NotificationHub>("/NotificationHub");
app.MapHub<ItemHub>("/itemHub"); // Map the SignalR Hub


// Top-level routing registration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
