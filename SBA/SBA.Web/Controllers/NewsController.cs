using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read(int id)
        {
            return View();
        }
    }
}