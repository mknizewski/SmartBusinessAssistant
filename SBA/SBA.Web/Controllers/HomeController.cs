using SBA.BOL.Web.Service;
using SBA.Web.Infrastructure.Alert;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly IConfigurationService _configurationService;

        public HomeController(IConfigurationService configurationService) =>
            _configurationService = configurationService;

        public virtual ActionResult Index()
        {
            SetAlert(SystemAlert.Type.Success, "Testowy alert");
            return View();
        }

        /// <summary>
        /// TODO: Do ogarniecia w późniejszym etapie.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(_configurationService.GetConfigurations());
        }
    }
}