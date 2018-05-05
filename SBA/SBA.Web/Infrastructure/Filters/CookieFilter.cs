using SBA.BOL.Common.Factory;
using SBA.BOL.Web.Service;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SBA.Web.Infrastructure.Filters
{
    public class CookieFilter : FilterAttribute, IActionFilter 
    {
        private readonly ICookieService _cookieService;
        private static string _statGuid => nameof(_statGuid);
        private static string _statTrace => nameof(_statTrace);

        public CookieFilter() =>
            _cookieService = MvcApplication.Container.GetInstance<ICookieService>();

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var statTraceList = (List<string>)filterContext.HttpContext.Session[_statTrace];
            var cookieData = new CookieService.CookieData
            {
                SessionId = filterContext.HttpContext.Session[_statGuid]?.ToString(),
                CurrentTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                ClientIp = filterContext.HttpContext.Request.UserHostAddress.ToString(),
                CurrentUrl = filterContext.HttpContext.Request.Url.ToString(),
                PreviousUrl = filterContext.HttpContext.Request.UrlReferrer?.ToString()
            };

            _cookieService.SaveToLog(cookieData);
            statTraceList.Add(cookieData.ToString());
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session[_statGuid] == null)
                filterContext.HttpContext.Session.Add(_statGuid, Guid.NewGuid().ToString());

            if (filterContext.HttpContext.Session[_statTrace] == null)
                filterContext.HttpContext.Session.Add(_statTrace, SimpleFactory.Get<List<string>>());
        }
    }
}