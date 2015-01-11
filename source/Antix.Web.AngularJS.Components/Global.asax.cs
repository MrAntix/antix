using System;
using System.Web;
using System.Web.Optimization;
using Antix.Web.AngularJS.Components.Configuration;

namespace Antix.Web.AngularJS.Components
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.Init(BundleTable.Bundles);
        }
    }
}