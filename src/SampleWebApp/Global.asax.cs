using System;
using System.Web.Optimization;
using UnderscoreBundler;

namespace SampleWebApp
{
    using System.Diagnostics;

    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            
            // Create frameworks bundle: JQuery and Underscore in our case
            var coreJs = new Bundle("~/core.js");
            coreJs.IncludeDirectory("~/scripts/", "*.js");            
            BundleTable.Bundles.Add(coreJs);

            Debugger.Launch();

            // Create app bundle: 
            //    main.html is underscore template
            //    app.js is our simple application
            var appJs = new Bundle("~/app.js");
            
            appJs.Include("~/scripts/app/main.html");
            appJs.Include("~/scripts/app/example2.html");
            appJs.Include("~/scripts/app/app.js");

            //appJs.Transforms.Add(new NoTransform("text/javascript; charset=utf-8"));
            // You can minify bundle if you want
            //cartJs.Transform = new JsMinify();
            appJs.Builder = new CompiledUndrescoreTemplatesBundler();

            BundleTable.Bundles.Add(appJs);
        }        
    }
}