using System.Linq;
using Emma.Core.Extensions;
using Emma.Core.Tests.Support;
using NUnit.Framework;
using Shouldly;

namespace Emma.Core.Tests
{
    public class TypeExtensionsTests
    {

        [Test]
        public void Can_identify_static_classes()
        {
            typeof(SampleExtensionsClass).IsStatic().ShouldBeTrue();
            typeof(NonStaticSampleClass).IsStatic().ShouldBeFalse();
        }
        
        [Test]
        public void Can_identify_all_extension_methods_in_class()
        {
            typeof(SampleExtensionsClass)
                .ExtensionMethods()
                .Count().ShouldBe(3);
        }

        [Test]
        public void Can_identify_all_extension_methods_in_assembly()
        {
            typeof(SampleExtensionsClass)
                .Assembly
                .ExtensionMethods()
                .Count().ShouldBe(9);

        }
    }
}