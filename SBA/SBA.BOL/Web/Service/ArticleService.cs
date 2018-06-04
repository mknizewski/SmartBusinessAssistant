using SBA.BOL.Web.Models;
using SBA.DAL.Context.WebDb.Entity;
using SBA.DAL.Context.WebDb.Repository.Articles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SBA.BOL.Web.Service
{
    public interface IArticleService
    {
        ArticlesModel GetArticlesByPage(int page, int itemPerPage = 5);
        ArticleModel GetArticleBy(int id);
        bool AddArticleFromJson(Dictionary<string, string> dictionary);
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

        public bool AddArticleFromJson(Dictionary<string, string> dictionary)
        {
            try
            {
                string appGuid = ConfigurationManager.AppSettings[nameof(appGuid)];
                if (dictionary["AppGuid"] != appGuid)
                    return false;

                _articleRepository.AddArticle(new Article
                {
                    Title = dictionary[nameof(Article.Title)],
                    Description = dictionary[nameof(Article.Description)],
                    Content = dictionary[nameof(Article.Content)],
                    InsertTime = DateTime.Now
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ArticleModel GetArticleBy(int id)
        {
            var article = _articleRepository.GetArticleBy(id);
            return new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Description = article.Description,
                InsertTime = article.InsertTime.ToString("yyy-MM-dd")
            };
        }


        public ArticlesModel GetArticlesByPage(int page, int itemPerPage = 5)
        {
            int totalCount = _articleRepository.GetArticles().Count();

            return new ArticlesModel
            {
                Articles = _articleRepository
                                .GetArticles()
                                .OrderByDescending(x => x.InsertTime)
                                .Skip((page - 1) * itemPerPage)
                                .Take(itemPerPage)
                                .ToList()
                                .Select(x => new ArticleModel
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    Description = x.Description,
                                    InsertTime = x.InsertTime.ToString("yyyy-MM-dd HH:mm")
                                }),
                Page = page,
                TotalCount = totalCount,
                TotalPages = totalCount / itemPerPage - (totalCount % itemPerPage == 0 ? 1 : 0)
            };
        }
    }
}
