﻿using SBA.Web.Infrastructure.BundleTransform;
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
                        "~/Scripts/TypeScript/helpers.js",
                        "~/bower_components/select2/dist/js/select2.min.js",
                        "~/Scripts/select2main.js"));

            // Użyj wersji deweloperskiej biblioteki Modernizr do nauki i opracowywania rozwiązań. Następnie, kiedy wszystko będzie
            // gotowe do produkcji, użyj narzędzia do kompilowania ze strony https://modernizr.com, aby wybrać wyłącznie potrzebne testy.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/mainCss").Include(
                      "~/Content/bootstrap.css",
                      "~/bower_components/font-awesome/css/font-awesome.css",
                      "~/bower_components/bootstrap-social/bootstrap-social.css",
                      "~/bower_components/select2/dist/css/select2.min.css",
                      "~/Content/site.css"));

            // Użycie Lessa, przykład
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

            // Minifikacja
            BundleTable.EnableOptimizations = true;
        }
    }
}
