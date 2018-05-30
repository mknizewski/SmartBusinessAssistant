using SBA.BOL.Web.Service;
using SBA.Web.Infrastructure.Filters;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public partial class NewsController : BaseController
    {
        private readonly IArticleService _articleService;

        public NewsController(IArticleService articleService) =>
            _articleService = articleService;

        [CookieFilter]
        public virtual ActionResult Index(int id = 1) =>
            View(_articleService.GetArticlesByPage(id));

        [CookieFilter]
        public virtual ActionResult Article(int id) =>
            View(_articleService.GetArticleBy(id));
    }
}