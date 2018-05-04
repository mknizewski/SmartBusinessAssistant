using SBA.BOL.Web.Service;
using System;
using System.Web.Mvc;

namespace SBA.Web.Infrastructure.Filters
{
    public class CookieFilter : FilterAttribute, IActionFilter
    {
        private readonly ICookieService _cookieService;
        private static string _statGuid => nameof(_statGuid);

        public CookieFilter() =>
            _cookieService = MvcApplication.Container.GetInstance<ICookieService>();

        public void OnActionExecuted(ActionExecutedContext filterContext) =>
            _cookieService.SaveToLog(new CookieService.CookieData
            {
                SessionId = filterContext.HttpContext.Session[_statGuid]?.ToString(),
                CurrentTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                ClientIp = filterContext.HttpContext.Request.UserHostAddress.ToString(),
                CurrentUrl = filterContext.HttpContext.Request.Url.ToString(),
                PreviousUrl = filterContext.HttpContext.Request.UrlReferrer?.ToString()
            });

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session[_statGuid] == null)
                filterContext.HttpContext.Session.Add(_statGuid, Guid.NewGuid().ToString());
        }
    }
}