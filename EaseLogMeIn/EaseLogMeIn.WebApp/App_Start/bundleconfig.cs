using System.Web;
using System.Web.Optimization;

namespace EaseLogMeIn.WebApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Globle Files
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-3.4.1.min.js",
                            "~/Scripts/jquery-ui.js",
                            "~/Scripts/Custom/Globle.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                   "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/CustomBootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/site.css")); 
            #endregion

            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
          "~/Scripts/Custom/Login.js"));


            bundles.Add(new ScriptBundle("~/bundles/Website").Include(
                      "~/Scripts/Custom/website.js"));
            bundles.Add(new ScriptBundle("~/bundles/Employee").Include(
                     "~/Scripts/Custom/employee.js"));
            bundles.Add(new ScriptBundle("~/bundles/Mapping").Include(
         "~/Scripts/Custom/mapping.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
