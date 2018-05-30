using SBA.DAL.Context.WebDb.Entity;
using System.Linq;

namespace SBA.DAL.Context.WebDb.Repository.Articles
{
    public interface IArticleRepository : IBaseRepository
    {
        IQueryable<Article> GetArticles();
        Article GetArticleBy(int id);
        void AddArticle(Article article);
    }

    public class ArticleRepository : BaseRepository, IArticleRepository
    {
        public void AddArticle(Article article)
        {
            Add(article);
            SaveChanges();
        }

        public Article GetArticleBy(int id) =>
            Queryable<Article>()
                .FirstOrDefault(x => x.Id == id);

        public IQueryable<Article> GetArticles() =>
            Queryable<Article>();
    }
}