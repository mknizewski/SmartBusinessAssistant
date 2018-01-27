using SBA.DAL.Context.WebDb.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.WebDb.Repository.Articles
{
    public interface IArticleRepository : IBaseRepository
    {
        IEnumerable<Article> GetArticles();
        Article GetArticleBy(int id);
    }

    public class ArticleRepository : BaseRepository, IArticleRepository
    {
        public Article GetArticleBy(int id) =>
            Queryable<Article>()
                .FirstOrDefault(x => x.Id == id);

        public IEnumerable<Article> GetArticles() =>
            Queryable<Article>()
                .ToList();
    }
}
