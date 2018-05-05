using SBA.BOL.Web.Service;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly IClientSocketService _clientSocketService;

        public HomeController(IClientSocketService clientSocketService) =>
            _clientSocketService = clientSocketService;

        public virtual ActionResult Index() => 
            View();

        public virtual ActionResult About() =>
            View();

        [HttpGet]
        public virtual async Task<JsonResult> GetFastLinks() => 
            Json(
                await _clientSocketService.SendStatTraceToGetHotLinks(GetStatTrace(), GetStatGuid()),
                JsonRequestBehavior.AllowGet);
    }
}