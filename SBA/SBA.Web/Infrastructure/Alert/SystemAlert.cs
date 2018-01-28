using System.Web.Mvc;

namespace SBA.Web.Infrastructure.Alert
{
    public static class SystemAlert
    {
        public static string SetAlert(
            Type type,
            string message)
        {
            var htmlString = new TagBuilder("div");
            string className = type == Type.Success ? "alert alert-success" :
                               type == Type.Info ? "alert alert-info" :
                               type == Type.Warning ? "alert alert-warning" :
                               "alert alert-danger";

            htmlString.AddCssClass(className);
            htmlString.SetInnerText(message);

            return htmlString.ToString(TagRenderMode.Normal);
        }

        public static MvcHtmlString RenderAlert(this HtmlHelper htmlHelper)
        {
            if (htmlHelper.ViewContext.TempData["alert"] is null)
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(htmlHelper.ViewContext.TempData["alert"].ToString());
        }

        public enum Type
        {
            Success,
            Info,
            Warning,
            Danger
        }
    }
}