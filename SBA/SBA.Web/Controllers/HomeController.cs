using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        public HomeController()
        { }

        public virtual ActionResult Index() => 
            View();

        public virtual ActionResult About() =>
            View();

        [HttpGet]
        public virtual Task<string> GetFastLinks()
        {
            
            return null;
        }
    }
}