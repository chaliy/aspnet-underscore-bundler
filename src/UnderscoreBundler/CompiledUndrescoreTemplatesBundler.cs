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
            //if (context == null)
            //{
            //    throw new ArgumentNullException("context");
            //}

            var buffer = new StringBuilder();
            //var id = "";
            //if (context.EnableInstrumentation)
            //{
            //    id = DefaultBundleBuilder.GetBoundaryIdentifier(bundle);
            //    stringBuilder.AppendLine(DefaultBundleBuilder.GenerateBundlePreamble(id));
            //}
            foreach (var current in files)
            {
                //if (context.EnableInstrumentation)
                //{
                //    stringBuilder.Append(DefaultBundleBuilder.GetFileHeader(appPath, current, DefaultBundleBuilder.GetInstrumentedFileHeaderFormat(id)));
                //}

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