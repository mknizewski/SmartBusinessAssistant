using SBA.BOL.Web.Service;
using SBA.Web.Infrastructure.Alert;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly IConfigurationService _configurationService;
        private readonly IClientSocketService _clientSocketService;

        public HomeController(
            IConfigurationService configurationService,
            IClientSocketService clientSocketService)
        {
            _configurationService = configurationService;
            _clientSocketService = clientSocketService;
        }

      
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
            ViewBag.Message = _clientSocketService.ExchangeDataWithCore("test_data");
            return View(_configurationService.GetConfigurations());
        }
    }
}