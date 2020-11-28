using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Emma.Core.Extensions;
using Emma.Core.Github;
using NUnit.Framework;

namespace Emma.Core.Tests
{
    public class ExtensionMethodParserTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Can_parse_Type()
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
        public void Can_parse_Assembly()
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

        [Test, Explicit("Hits the github api, use for debugging/development purposes")]
        public void Can_parse_github_repo()
        {
            var user = "chrislee187";
            var repo = "Emma";
            var loc = new GithubLocation(user, repo);
            var extensionsFromFolderInGit = ExtensionMethodParser
                .Parse(new GithubRepoFolder(Credentials.AppKey(), loc))
                .OrderBy(i => i.ToString())
                .ToArray();

            var extensionsFromAssemblies = ExtensionMethodParser.Parse(typeof(ReflectionExtensions).Assembly)
                .Concat(ExtensionMethodParser.Parse(typeof(SampleExtensionsClass).Assembly))
                .OrderBy(i => i.ToString())
                .ToArray();

            ConsoleDumpMethods(extensionsFromFolderInGit, "From Git");
            ConsoleDumpMethods(extensionsFromAssemblies, "From Assembly");

            CollectionAssert.AreEquivalent(extensionsFromFolderInGit, extensionsFromAssemblies);


        }

        private static void ConsoleDumpMethods(IEnumerable<ExtensionMethod> methods, string source)
        {
            
            Console.WriteLine();
            Console.WriteLine(source);
            foreach (var mi in methods)
            {
                Console.WriteLine($"{mi}");
            }
        }
    }
}