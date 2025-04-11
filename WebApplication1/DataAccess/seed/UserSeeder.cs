using Microsoft.AspNetCore.Identity;
using WebApplication1.Identity;

namespace WebApplication1
{
    public class UserSeeder : IDbSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSeeder(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedAsync(AppDbContext context)
        {
            var adminEmail = "admin@example.com";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                };

                await _userManager.CreateAsync(adminUser, "Admin123!");

                await _userManager.AddToRoleAsync(adminUser, "Administrateur");
            }
        }
    }
}
