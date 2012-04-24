ASP.NET Underscore Bundler
==========================

Management of the templates for your javascript app could be pain. Of course, there are few [solutions](http://samarskyy.blogspot.com/2012/03/loading-external-jquery-template-files.html) [out](http://encosia.com/jquery-templates-composite-rendering-and-remote-loading/) [there](http://www.knockmeout.net/2011/03/using-external-jquery-template-files.html). This library is another solution :). Okay, sort of. Really this is just better implementation of [Anton Samarskyy's idea](http://samarskyy.blogspot.com/2012/03/loading-external-jquery-template-files.html).

So main idea of this library is about to keep templates as `HTML` files near of your `JS` files, but serve them as single file to reduce requests. What is more intersting is that we can go further precompile templates so we can serve templates as single `JS` file with our app scripts. Stay turned.

Now about implmentation. ASP.NET MVC 4 introduced [bunndling](http://msdn.microsoft.com/en-us/library/system.web.optimization.bundle.aspx) support. In simple words this is actually means combining (and minifying) many resource files (javascript, css) to the single file to reduce number of requests. This is what we actually use.

Features
========

1. Bundle HTML templates to the single file
2. Precompile Underscore templates (ver 1.3.3)

Example
=======

Assuming that you have folder structure like this 

![Folder structure]("/docs/bundlefoldertructure.png")

bundle configuration will be

    // Create app bundle: 
    //    main.html and example2.html are underscore templates
    //    app.js is our simple application
    var cartJs = new Bundle("~/scripts/app.js");
    
    cartJs.AddFile("~/scripts/app/main.html");
    cartJs.AddFile("~/scripts/app/example2.html");      
    cartJs.AddFile("~/scripts/app/app.js");
    
    cartJs.Transform = new NoTransform("text/javascript; charset=utf-8");    
    //cartJs.Transform = new JsMinify(); // You can minify bundle if you want
    cartJs.Builder = new CompiledUndrescoreTemplatesBundler();
    
    BundleTable.Bundles.Add(cartJs);

Complete example is SampleWebApp project in sources.

Installation
============
	
<div class="nuget-badge">
    <p><code>PM&gt; Install-Package UnderscoreBundler</code></p>
</div>]
	
License
=======

Licensed under the MIT
