namespace WebApplication1
{
    public class ArticleSeeder : IDbSeeder
    {
        public async Task SeedAsync(AppDbContext context)
        {
            if (!context.Articles.Any())
            {
                context.Articles.AddRange(
                    new Article { Title = "Article 1", Content = "Contenu du premier article" },
                    new Article { Title = "Article 2", Content = "Contenu du deuxième article" },
                    new Article { Title = "Article 3", Content = "Contenu du troisième article" }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
