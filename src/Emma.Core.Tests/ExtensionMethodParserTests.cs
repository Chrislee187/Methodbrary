using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using NUnit.Framework;
using Shouldly;

namespace Emma.Core.Tests
{
    public class ExtensionMethodParserTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Can_parse_type()
        {
            var type = typeof(SampleExtensionsClass);

            var extensionMethodInfos = type
                .ExtensionMethods()
                .Select(ExtensionMethodParser.Parse)
                .ToList();

            foreach (var mi in extensionMethodInfos)
            {
                Console.WriteLine($"{mi}");
            }
        }

        [Test]
        public void Can_parse_assembly()
        {
            var type = typeof(SampleExtensionsClass);
            
            foreach (var mi in ExtensionMethodParser.Parse(type.Assembly))
            {
                Console.WriteLine($"{mi}");
            }
        }

        [Test]
        public void Can_parse_source_code()
        {
            var csFile = File.ReadAllText(@"..\..\..\SampleExtensionsClass.cs");

            foreach (var mi in ExtensionMethodParser
                .Parse(csFile))
            {
                Console.WriteLine($"{mi}");
            }
        }
    }
}