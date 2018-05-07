using SBA.BOL.Web.Service;
using SBA.Web.Infrastructure.Filters;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly IClientSocketService _clientSocketService;

        public HomeController(IClientSocketService clientSocketService) =>
            _clientSocketService = clientSocketService;

        [CookieFilter]
        public virtual ActionResult Index() => 
            View();

        [CookieFilter]
        public virtual ActionResult About() =>
            View();

        [HttpGet]
        public virtual async Task<JsonResult> GetFastLinks() => 
            Json(
                await _clientSocketService.SendStatTraceToGetHotLinks(GetStatTrace(), GetStatGuid()),
                JsonRequestBehavior.AllowGet);
    }
}