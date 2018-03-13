using SBA.BOL.Web.Models;
using SBA.DAL.Context.WebDb.Repository.Articles;
using System.Collections.Generic;
using System.Linq;

namespace SBA.BOL.Web.Service
{
    public interface IArticleService
    {
        IEnumerable<ArticleModel> GetArticlesByPage(int page = 3);
        ArticleModel GetArticleBy(int id);
    }

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFileService _fileService;

        public ArticleService(
            IArticleRepository articleRepository,
            IFileService fileService)
        {
            _articleRepository = articleRepository;
            _fileService = fileService;
        }

        public ArticleModel GetArticleBy(int id)
        {
            var article = _articleRepository.GetArticleBy(id);
            return new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = _fileService.GetFileContent(article.Path),
                InsertTime = article.InsertTime.ToString("yyy-MM-dd")
            };
        }


        public IEnumerable<ArticleModel> GetArticlesByPage(int page = 3) =>
            _articleRepository
                .GetArticles()
                .OrderByDescending(x => x.InsertTime)
                .Take(page)
                .Select(x => new ArticleModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = _fileService.GetFileContent(x.Path),
                    InsertTime = x.InsertTime.ToString("yyyy-MM-dd")
                });
    }
}
