namespace UnderscoreBundler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web.Optimization;

    public class CompiledUndrescoreTemplatesBundler : IBundleBuilder
    {
        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            if (files == null)
            {
                return string.Empty;
            }

            var buffer = new StringBuilder();

            foreach (var current in files)
            {
                buffer.AppendLine();

                var extension = Path.GetExtension(current.VirtualFile.Name) ?? "";
                var fileContent = RetrieveFileContent(current);

                if (extension.Equals(".html", StringComparison.OrdinalIgnoreCase)
                    || extension.Equals(".htm", StringComparison.OrdinalIgnoreCase))
                {

                    var templateName = Path.GetFileNameWithoutExtension(current.VirtualFile.Name);

                    var compiler = new UnderscoreTemplateCompiler();

                    buffer.AppendLine("var " + templateName + "Tpl = " + compiler.Compile(fileContent));
                }
                else
                {
                    buffer.AppendLine(fileContent);
                }
            }
            return buffer.ToString();
        }

        static string RetrieveFileContent(BundleFile current)
        {
            using (var stream = current.VirtualFile.Open())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}