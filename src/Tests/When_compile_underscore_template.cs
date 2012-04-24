using NUnit.Framework;
using FluentAssertions;

namespace UnderscoreBundler.Tests
{
    public class When_compile_underscore_template
    {
        [Test]
        public void Should_build_something_most_simple()
        {
            var compiler = new UnderscoreTemplateCompiler();
            var result = compiler.BuildSource("<span>212</span>");

            result.Should().Be(
                @"var __t,__p='',__j=Array.prototype.join,print=function(){__p+=__j.call(arguments,'')};
with(obj||{}){
__p+='<span>212</span>';
}
return __p;
");
        }

        [Test]
        public void Should_build_with_variables()
        {
            var compiler = new UnderscoreTemplateCompiler();
            var result = compiler.BuildSource("<span><%= name %></span>");

            result.Should().Be(
                @"var __t,__p='',__j=Array.prototype.join,print=function(){__p+=__j.call(arguments,'')};
with(obj||{}){
__p+='<span>'+
((__t=( name ))==null?'':__t)+
'</span>';
}
return __p;
");
        }

        [Test]
        public void Should_build_with_executables()
        {
            var compiler = new UnderscoreTemplateCompiler();
            var result = compiler.BuildSource("<span><% print('Hello ' + epithet); %></span>");

            result.Should().Be(
                @"var __t,__p='',__j=Array.prototype.join,print=function(){__p+=__j.call(arguments,'')};
with(obj||{}){
__p+='<span>';
 print('Hello ' + epithet); 
;__p+='</span>';
}
return __p;
");
        }

        [Test]
        public void Should_build_with_escape()
        {
            var compiler = new UnderscoreTemplateCompiler();
            var result = compiler.BuildSource("<span><%- <span>A</span> %></span>");

            result.Should().Be(
                @"var __t,__p='',__j=Array.prototype.join,print=function(){__p+=__j.call(arguments,'')};
with(obj||{}){
__p+='<span>'+
((__t=( <span>A</span> ))==null?'':_.escape(__t))+
'</span>';
}
return __p;
");
        }
    }
}