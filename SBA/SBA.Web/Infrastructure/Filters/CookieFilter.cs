using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Mvc;


namespace SBA.Web.Infrastructure.Filters
{
    public class CookieFilter : FilterAttribute, IActionFilter 
    {
        private Stopwatch _timer = null;

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // TODO: Fill  
            string currentIPSerwer = filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"].ToString();
            string currentIPClient = filterContext.HttpContext.Request.UserHostAddress.ToString();
            string currentURL = filterContext.HttpContext.Request.Url.ToString();
            string lastURL = "None";
            DateTime currentDateTime = DateTime.Now;
            string currentDate = currentDateTime.Date.ToShortDateString();

            if (filterContext.HttpContext.Request.UrlReferrer != null)
            {
                lastURL = filterContext.HttpContext.Request.UrlReferrer.ToString();
            }

            //filterContext.HttpContext.Response.Write(
            //    string.Format("<p>Czas wykonania akcji to: {0:F6} sekundy</p>"+
            //        "<p><strong>Ip serwera to: </strong>{1}</p>" +
            //        "<p><strong>IP klienta to </strong>{2}</p>" +
            //        "<p><strong>Obecny URL to </strong>{3}</p>" +
            //        "<p><strong>Poprzedni URL to </strong>{4}</p>" +
            //        "<p><strong>Czas </strong>{5}</p>",
            //    _timer.Elapsed.TotalSeconds, currentIPSerwer, currentIPClient, currentURL, lastURL, currentDate)
            //);

            //save to csv
            string fileName = "web.log";
            string filePath = @"D:\git_folder\logs\web\" +  currentDate;
            Directory.CreateDirectory(filePath);

            var file = new StringBuilder();
            var line = string.Format("{0} {1} {2} {3}\n", 
                currentIPClient, currentDateTime, currentURL, lastURL);
            file.Append(line);
            File.AppendAllText(filePath + @"\" + fileName, file.ToString());
        }


        public void OnActionExecuting(ActionExecutingContext filterContext)
        {   }

    }
}