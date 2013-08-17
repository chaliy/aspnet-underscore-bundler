namespace UnderscoreBundler
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Line by line port of the Underscore's template method.
    /// </summary>
    public class UnderscoreTemplateCompiler
    {
        readonly Regex _evaluate = new Regex("<%([\\s\\S]+?)%>");
        readonly Regex _interpolate = new Regex("<%=([\\s\\S]+?)%>");
        readonly Regex _escape = new Regex("<%-([\\s\\S]+?)%>");
        readonly string _variable;

        readonly Regex _escaper = new Regex("\\\\|'|\r|\n|\t|\u2028|\u2029");
        readonly Regex _unescaper = new Regex("\\\\(\\\\|'|r|n|t|u2028|u2029)");
        // When customizing `templateSettings`, if you don't want to define an
        // interpolation, evaluation or escaping regex, we need one that is
        // guaranteed not to match.
        static readonly Regex NoMatch = new Regex(".^");


        private static readonly IDictionary<string, string> Unescapes = new Dictionary<string, string>
        {
            {"\\", "\\"},
            {"'", "'"},
            {"r", "\r"},
            {"n", "\n"},
            {"t", "\t"},
            {"u2028", "\u2028"},
            {"u2029", "\u2029"}
        };

        private static readonly IDictionary<string, string> Escapes = new Dictionary<string, string>
        {
            {"\\", "\\"},
            {"'", "'"},
            {"\r", "r"},
            {"\n", "n"},
            {"\t", "t"},
            {"\u2028", "u2028"},
            {"\u2029", "u2029"}
        };

        // Within an interpolation, evaluation, or escaping, remove HTML escaping
        // that had been previously added.
        private string Unescape(string code)
        {
            return code.Replace(_unescaper, (match, escape2) => Unescapes[escape2]);
        }
        
        /// <summary>
        /// Returns compiled to javascript template.
        /// </summary>
        /// <remarks>
        /// JavaScript micro-templating, similar to John Resig's implementation.
        /// Underscore templating handles arbitrary delimiters, preserves whitespace,
        /// and correctly escapes quotes within interpolated code.
        /// </remarks>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Compile(string text/*, string data, string settings*/)
        {
            //settings = _.defaults(settings || {}, _.templateSettings);

            var source = BuildSource(text);

            //var render = new Function(settings.variable || 'obj', '_', source);
            //if (data) return render(data, _);
            //var template = function(data) {
            //   return render.call(this, data, _);
            //};

            //  // Provide the compiled function source as a convenience for precompilation.
            //  template.source = 'function(' + (settings.variable || 'obj') + '){\n' + source + '}';

            //  return template;
            //};";

            // TODO Provide source property
            
            return "function (data) {\n"
                   + "return (function (" + (_variable ?? "obj") + ", _) {\n"
                   + source
                   + "}).call(this, data, _)\n"
                   + "}";
        }

        public string BuildSource(string text)
        {
            // Compile the template source, taking care to escape characters that
            // cannot be included in a string literal and then unescape them in code
            // blocks.

            var source = "__p+='" + text
                .Replace(_escaper, match =>
                {
                    return "\\" + Escapes[match];
                })
                .Replace(_escape ?? NoMatch, (match, code) =>
                {
                    return "'+\n((__t=(" + Unescape(code) +
                            "))==null?'':_.escape(__t))+\n'";
                })
                .Replace(_interpolate ?? NoMatch, (match, code) =>
                {
                    return "'+\n((__t=(" + Unescape(code) +
                            "))==null?'':__t)+\n'";
                })
                .Replace(_evaluate ?? NoMatch, (match, code) =>
                {
                    return "';\n" + Unescape(code) +
                            "\n;__p+='";
                }) + "';\n";


            //  // If a variable is not specified, place data values in local scope.
            if (String.IsNullOrWhiteSpace(_variable))
            {
                source = "with(obj||{}){\n" + source + "}\n";
            }

            source = "var __t,__p='',__j=Array.prototype.join," +
                     "print=function(){__p+=__j.call(arguments,'')};\n" +
                     source + "return __p;\n";
            return source;
        }        
    }
}