using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public partial class NewsController : BaseController
    {
        public virtual ActionResult Index() =>
            View();

        public virtual ActionResult Read(int id) => 
            View();
    }
}