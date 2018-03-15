using SBA.Web.Infrastructure.BundleTransform;
using System.Web.Optimization;

namespace SBA.Web
{
    public class BundleConfig
    {
        // Aby uzyskać więcej informacji o grupowaniu, odwiedź stronę https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/mainJs").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/TypeScript/main.js",
                        "~/Scripts/TypeScript/helpers.js"));

            // Użyj wersji deweloperskiej biblioteki Modernizr do nauki i opracowywania rozwiązań. Następnie, kiedy wszystko będzie
            // gotowe do produkcji, użyj narzędzia do kompilowania ze strony https://modernizr.com, aby wybrać wyłącznie potrzebne testy.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/mainCss").Include(
                      "~/Content/bootstrap.css",
                      "~/bower_components/font-awesome/css/font-awesome.css",
                      "~/bower_components/bootstrap-social/bootstrap-social.css",
                      "~/Content/site.css"));

            // Użycie Lessa, przykład
            var lessBundle = new Bundle("~/Content/mainLess").Include(
                "~/Content/Less/style.less");
            lessBundle.Transforms.Add(new LessTransform());
            lessBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessBundle);

            var lessHomeIndexBundle = new Bundle("~/Content/homeIndex").Include(
                "~/Content/Less/homeIndex.less");
            lessHomeIndexBundle.Transforms.Add(new LessTransform());
            lessHomeIndexBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessHomeIndexBundle);

            // Minifikacja
            BundleTable.EnableOptimizations = true;
        }
    }
}
