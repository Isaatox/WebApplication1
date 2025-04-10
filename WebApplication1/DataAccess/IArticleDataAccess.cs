namespace WebApplication1
{
    public interface IArticleDataAccess
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task<Article> CreateAsync(Article article);
        Task<bool> DeleteAsync(int id);
    }
}
