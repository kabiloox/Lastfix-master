using System.Web;
using System.Web.Optimization;

namespace Electronique_Labo
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(                                                                                  
                        "~/Scripts/umd/popper.min.js",
                        "~/Scripts/Slick/slick.min.js",
                "~/Scripts/chosen.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/jquery-{version}.js",
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Slick/slick-theme.css",
                "~/Content/Slick/slick.css",
                      "~/Content/bootstrap-lumen.css",                                        
                      "~/Content/site.css",
                "~/Content/chosen/chosen.min.css"));
        }
    }
}
