using System;
using System.Web;
using System.Web.Optimization;
using Antix.Code.Web.Configuration;

namespace Antix.Code.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.Init(BundleTable.Bundles);
        }
    }
}