using DevExpress.PivotGrid.OLAP.Mdx;
using Microsoft.AspNetCore.Identity;
using ServiceMaintenance.Seeds;
using ServiceMaintenance;

namespace ServiceMaintenance.Seeds
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerProvider>();
            var logger = loggerFactory.CreateLogger("app");

            try
            {
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await DefaultRoles.SeedAsync(roleManager);
                await DefaultUsers.SeedBasicUserAsync(userManager);
                await DefaultUsers.SeedSuperAdminUserAsync(userManager, roleManager);

                logger.LogInformation("Data seeded");
                logger.LogInformation("Application Started");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred while seeding data");
            }
        }
    }
}


/*// Seeding roles and users
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerProvider>();
var logger = loggerFactory.CreateLogger("app");

try
{
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DefaultRoles.SeedAsync(roleManager);
    await DefaultUsers.SeedBasicUserAsync(userManager);
    await DefaultUsers.SeedSuperAdminUserAsync(userManager, roleManager);

    logger.LogInformation("Data seeded");
    logger.LogInformation("Application Started");
}
catch (System.Exception ex)
{
    logger.LogWarning(ex, "An error occurred while seeding data");
}*/