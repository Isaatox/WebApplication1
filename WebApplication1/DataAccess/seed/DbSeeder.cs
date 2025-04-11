using Microsoft.AspNetCore.Identity;
using WebApplication1.Identity;
using WebApplication1.Seeders;

namespace WebApplication1
{
    public static class DbSeeder
    {
        public static async Task SeedDatabaseAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<
                UserManager<ApplicationUser>
            >();
            var roleManager = scope.ServiceProvider.GetRequiredService<
                RoleManager<ApplicationRole>
            >();

            await context.Database.EnsureCreatedAsync();

            var seeders = new List<IDbSeeder>
            {
                new RoleSeeder(),
                new UserSeeder(userManager),
                new ServiceTypeSeeder(),
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(context);
            }
        }
    }
}
