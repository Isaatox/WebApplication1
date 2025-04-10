namespace WebApplication1
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleDataAccess _dataAccess;

        public ArticleService(IArticleDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<Article>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<Article?> GetByIdAsync(int id) => _dataAccess.GetByIdAsync(id);

        public Task<Article> CreateAsync(Article article) => _dataAccess.CreateAsync(article);

        public Task<bool> DeleteAsync(int id) => _dataAccess.DeleteAsync(id);
    }
}
