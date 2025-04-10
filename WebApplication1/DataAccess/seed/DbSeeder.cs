namespace WebApplication1
{
    public static class DbSeeder
    {
        public static async Task SeedDatabaseAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.EnsureCreatedAsync();

            var seeders = new List<IDbSeeder> { new ArticleSeeder() };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(context);
            }
        }
    }
}
