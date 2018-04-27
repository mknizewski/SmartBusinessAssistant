using SBA.Web.Infrastructure.BundleTransform;
using System.Web.Optimization;

namespace SBA.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/mainJs").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/TypeScript/main.js",
                        "~/Scripts/TypeScript/helpers.js",
                        "~/bower_components/select2/dist/js/select2.min.js",
                        "~/Scripts/select2main.js",
                        "~/Scripts/clicksData.js",
                        "~/Scripts/shared.js",
                        "~/Scripts/scrollAction.js",
                        "~/Scripts/getArticle.js"));

            bundles.Add(new ScriptBundle("~/bundles/contactJs").Include(
                "~/Scripts/TypeScript/contact.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/mainCss").Include(
                      "~/Content/bootstrap.css",
                      "~/bower_components/bootstrap-social/bootstrap-social.css",
                      "~/bower_components/select2/dist/css/select2.min.css"));

            var lessBundle = new Bundle("~/Content/mainLess").Include(
                "~/Content/Less/style.less");
            lessBundle.Transforms.Add(new LessTransform());
            lessBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessBundle);

            var lessHomeStyleBundle = new Bundle("~/Content/homeStyle").Include(
                "~/Content/Less/homeStyle.less");
            lessHomeStyleBundle.Transforms.Add(new LessTransform());
            lessHomeStyleBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessHomeStyleBundle);

            var lessContactStyleBundle = new Bundle("~/Content/contactStyle").Include(
                "~/Content/Less/contactStyle.less");
            lessContactStyleBundle.Transforms.Add(new LessTransform());
            lessContactStyleBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessContactStyleBundle);

            var lessNewsStyleBundle = new Bundle("~/Content/newsStyle").Include(
                "~/Content/Less/newsStyle.less");
            lessNewsStyleBundle.Transforms.Add(new LessTransform());
            lessNewsStyleBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessNewsStyleBundle);

            var lessAboutStyleBundle = new Bundle("~/Content/aboutStyle").Include(
                "~/Content/Less/aboutStyle.less");
            lessAboutStyleBundle.Transforms.Add(new LessTransform());
            lessAboutStyleBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessAboutStyleBundle);

            BundleTable.EnableOptimizations = false;
        }
    }
}