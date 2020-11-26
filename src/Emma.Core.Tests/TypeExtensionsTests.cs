using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Emma.Core.Tests
{
    public class TypeExtensionsTests
    {

        [Test]
        public void TypeExtension_IsStaticWorks()
        {
            var type = typeof(SampleExtensionsClass);

            type.IsStatic().ShouldBeTrue();
        }


        [Test]
        public void TypeExtension_FindsExtensionMethodsInType()
        {
            var type = typeof(SampleExtensionsClass);

            var extensionMethodInfos = type
                .ExtensionMethods()
                .Select(ExtensionMethodParser.Parse)
                .ToList();

            extensionMethodInfos.Count().ShouldBe(3);
            
            foreach (var mi in extensionMethodInfos)
            {
                Console.WriteLine($"{mi}");
            }
        }

        [Test]
        public void TypeExtension_FindsAllExtensionsInAssembly()
        {
            var type = typeof(SampleExtensionsClass);

            var extensionMethodInfos = type.Assembly
                .ExtensionMethods()
                .ToList();

            extensionMethodInfos.Count().ShouldBe(5);

        }

        [Test]
        public void TypeExtension_IgnoresNonStaticClasses()
        {
            var type = typeof(NonStaticSampleClass);

            var extensionMethodInfos = type
                .ExtensionMethods()
                .ToList();

            extensionMethodInfos.Count().ShouldBe(0);

        }

    }

    public class NonStaticSampleClass
    {

    }
}