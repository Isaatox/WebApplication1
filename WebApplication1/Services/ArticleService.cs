using Microsoft.Extensions.Caching.Memory;

namespace WebApplication1
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleDataAccess _dataAccess;
        private readonly IMemoryCache _cache;

        public ArticleService(IArticleDataAccess dataAccess, IMemoryCache cache)
        {
            _dataAccess = dataAccess;
            _cache = cache;
        }

        // public Task<IEnumerable<Article>> GetAllAsync() => _dataAccess.GetAllAsync();
        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            const string cacheKey = "articles_cache";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<Article> cachedArticles))
            {
                return cachedArticles;
            }

            var articles = await _dataAccess.GetAllAsync();

            _cache.Set(cacheKey, articles, TimeSpan.FromMinutes(5));

            return articles;
        }

        public Task<Article?> GetByIdAsync(int id) => _dataAccess.GetByIdAsync(id);

        public Task<Article> CreateAsync(Article article) => _dataAccess.CreateAsync(article);

        public Task<bool> DeleteAsync(int id) => _dataAccess.DeleteAsync(id);
    }
}
