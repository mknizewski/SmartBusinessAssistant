using SBA.Web.Infrastructure.Filters;
using System.Web;
using System.Web.Mvc;

namespace SBA.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CookieFilter());
        }
    }
}
