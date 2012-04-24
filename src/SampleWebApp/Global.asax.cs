using System;
using System.Web.Optimization;
using UnderscoreBundler;

namespace SampleWebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            
            // Create frameworks bundle: JQuery and Underscore in our case
            var coreJs = new Bundle("~/scripts/core.js", new JsMinify());            
            //coreJs.AddDirectory("~/scripts/", "*.min.js");
            BundleTable.Bundles.Add(coreJs);

            // Create app bundle: 
            //    main.html is underscore template
            //    app.js is our simple application
            var cartJs = new Bundle("~/scripts/app.js");
            
            cartJs.AddFile("~/scripts/app/main.html");
            cartJs.AddFile("~/scripts/app/example2.html");
            cartJs.AddFile("~/scripts/app/app.js");

            cartJs.Transform = new NoTransform("text/javascript; charset=utf-8");
            // You can minify bundle if you want
            //cartJs.Transform = new JsMinify();
            cartJs.Builder = new CompiledUndrescoreTemplatesBundler();

            BundleTable.Bundles.Add(cartJs);
        }        
    }
}