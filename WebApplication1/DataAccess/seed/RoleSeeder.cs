using WebApplication1.Identity;

namespace WebApplication1
{
    public class RoleSeeder : IDbSeeder
    {
        public async Task SeedAsync(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new ApplicationRole
                    {
                        Name = "Client",
                        NormalizedName = "CLIENT",
                        Description = "Accès de base pour les clients.",
                    },
                    new ApplicationRole
                    {
                        Name = "Technicien",
                        NormalizedName = "TECHNICIEN",
                        Description = "Accès pour les techniciens pouvant gérer les interventions.",
                    },
                    new ApplicationRole
                    {
                        Name = "Administrateur",
                        NormalizedName = "ADMINISTRATEUR",
                        Description = "Accès complet à l’administration du système.",
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
