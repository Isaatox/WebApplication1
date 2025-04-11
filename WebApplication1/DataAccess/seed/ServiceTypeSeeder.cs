namespace WebApplication1.Seeders
{
    public class ServiceTypeSeeder : IDbSeeder
    {
        public async Task SeedAsync(AppDbContext context)
        {
            if (!context.Services.Any())
            {
                context.Services.AddRange(
                    new ServiceType
                    {
                        Nom = "Chauffage",
                        Description = "Services liés au chauffage.",
                    },
                    new ServiceType
                    {
                        Nom = "Réseau",
                        Description = "Infrastructure réseau et internet.",
                    },
                    new ServiceType
                    {
                        Nom = "Électricité",
                        Description = "Installations et pannes électriques.",
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
