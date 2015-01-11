using System.Web.Optimization;

namespace Antix.Code.Web.Configuration
{
    public static class BundleConfig
    {
        public static void Init(BundleCollection bundles)
        {
            bundles
                .Add(new StyleBundle("~/bundles/style")
                    .IncludeCommonStyle()
                );

            bundles.Add(
                new ScriptBundle("~/bundles/script")
                    .IncludeCommonScripts()
                    .IncludeDirectory("~/angularjs-components/", "*.js", true)
                );

            //BundleTable.EnableOptimizations = true;
        }

        public static T IncludeCommonStyle<T>(this T bundle)
            where T : Bundle
        {
            bundle
                .Include("~/Content/animate.css")
                .IncludeDirectory("~/Content/site/", "*.css", true)
                .IncludeDirectory("~/Scripts/antix/", "*.css", true)
                .IncludeDirectory("~/angularjs-components/", "*.css", true);

            return bundle;
        }

        public static T IncludeCommonScripts<T>(this T bundle)
            where T : Bundle
        {
            bundle
                .Include("~/Scripts/moment.js")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-sanitize.js")
                .Include("~/Scripts/angular-mocks.js")
                .Include("~/Scripts/angular-resource.js")
                .Include("~/Scripts/angular-cookies.js")
                .Include("~/Scripts/angular-animate.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
                .Include("~/Scripts/angular-ui/ui-utils.js")
                .Include("~/Scripts/angular-ui-router.js")
                .IncludeDirectory("~/Scripts/other", "*.js", true)
                .IncludeDirectory("~/Scripts/antix", "*.js", true);

            return bundle;
        }
    }
}