using System;
using System.Collections.Generic;
using System.Linq;
using Emma.Core.Extensions;
using Emma.Core.MethodSources;
using Emma.Core.Tests.Support;
using NUnit.Framework;
using Shouldly;

namespace Emma.Core.Tests
{
    public class ExtensionMethodLibraryTests
    {
        private ExtensionMethodLibrary _library;

        public class TestSource : ExtensionMethodsSource
        {
            public TestSource(IEnumerable<ExtensionMethod> methods)
            {
                this.Methods = methods;
                this.LastUpdated = DateTime.Now;
            }
        }
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var extensionMethods = ExtensionMethodParser
                .Parse(typeof(SampleExtensionsClass).ExtensionMethods(), DateTime.Now);
            _library = new ExtensionMethodLibrary(new TestSource(extensionMethods));
            ConsoleX.Dump(_library.Methods, "Test Library Contents");
        }

        [Test]
        public void Name_can_search_by_exact()
        {
            var results = _library.FindByName("ToNowhere", StringMatchMode.Equals);
            results.Count().ShouldBe(2);

            results = _library.FindByName("ToNow", StringMatchMode.Equals);
            results.Count().ShouldBe(0);
        }

        [Test]
        public void Name_can_search_by_start_of()
        {
            var results = _library.FindByName("ToNow", StringMatchMode.StartsWith);
            results.Count().ShouldBe(2);

            results = _library.FindByName("where", StringMatchMode.StartsWith);
            results.Count().ShouldBe(0);
        }

        [Test]
        public void Name_can_search_by_end_of()
        {
            var results = _library.FindByName("where", StringMatchMode.EndsWith);
            results.Count().ShouldBe(2);

            results = _library.FindByName("ToNow", StringMatchMode.EndsWith);
            results.Count().ShouldBe(0);
        }

        [Test]
        public void Name_search_by_substring()
        {
            var results = _library.FindByName("owhe");
            results.Count().ShouldBe(2);

            results = _library.FindByName("xyz");
            results.Count().ShouldBe(0);
        }

        [Test]
        public void ExtendingType_can_search_by()
        {
            var results = _library.FindByExtendingType("object");
            results.Count().ShouldBe(3);

            results = _library.FindByName("NotAType");
            results.Count().ShouldBe(0);
        }

        [Test]
        public void ReturningType_can_search_by()
        {
            var results = _library.FindByReturnType("string");
            results.Count().ShouldBe(1);

            results = _library.FindByReturnType("NotAType");
            results.Count().ShouldBe(0);
        }
        [Test]
        public void ParamTypes_can_search_by()
        {
            var results = _library.FindByParamTypes(new []{"float","string"});
            results.Count().ShouldBe(1);

            results = _library.FindByParamTypes(new[] { "NotAType" });
            results.Count().ShouldBe(0);
        }
    }
}