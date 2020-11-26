using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using NUnit.Framework;
using Shouldly;

namespace Emma.Core.Tests
{
    public class Spikes
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Spike1()
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
        public void Spike2()
        {
            var type = typeof(SampleExtensionsClass);
            
            foreach (var mi in ExtensionMethodParser.Parse(type.Assembly))
            {
                Console.WriteLine($"{mi}");
            }
        }

        [Test]
        public void Spike()
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