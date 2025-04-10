namespace WebApplication1
{
    public interface IDbSeeder
    {
        Task SeedAsync(AppDbContext context);
    }
}
