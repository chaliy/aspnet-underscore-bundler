using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Optimization;

namespace UnderscoreBundler
{    
    public class CompiledUndrescoreTemplatesBundler : IBundleBuilder
    {
        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<FileInfo> files)
        {
            if (files == null)
            {
                return string.Empty;
            }
            
            var buffer = new StringBuilder();
            
            foreach (var current in files)
            {
                if (current.Extension.Equals(".html", StringComparison.OrdinalIgnoreCase))
                {
                    var compiler = new UnderscoreTemplateCompiler();
                    buffer.AppendLine("var " + Path.GetFileNameWithoutExtension(current.Name) + "Tpl = " + compiler.Compile(File.ReadAllText(current.FullName)));
                }
                else
                {
                    buffer.AppendLine(File.ReadAllText(current.FullName));   
                }                
            }
            return buffer.ToString();
        }        
    }
}