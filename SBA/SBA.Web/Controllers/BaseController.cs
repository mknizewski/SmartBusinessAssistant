using SBA.Web.Infrastructure.Alert;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public abstract partial class BaseController : Controller
    {
        public void SetAlert(
            SystemAlert.Type type,
            string message) =>
            TempData["alert"] = SystemAlert.SetAlert(type, message);

        public string GetStatGuid() =>
            HttpContext.Session["_statGuid"]?.ToString();

        public List<string> GetStatTrace() =>
            (List<string>)HttpContext.Session["_statTrace"];
    }
}