using SBA.BOL.Web.Service;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigurationService _configurationService;

        public HomeController(IConfigurationService configurationService) => 
            _configurationService = configurationService;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(_configurationService.GetConfigurations());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}