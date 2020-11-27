using System;
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

            var extensionsFromFolderInGit = ExtensionMethodParser
                .Parse(new GithubRepoFolder(user, repo, Credentials.AppKey(), "src/Emma.Core"));

            var extensionsFromAssembly = ExtensionMethodParser.Parse(typeof(ReflectionExtensions).Assembly);

            CollectionAssert.AreEquivalent(extensionsFromFolderInGit, extensionsFromAssembly);

            foreach (var mi in extensionsFromFolderInGit)
            {
                Console.WriteLine($"{mi}");
            }
        }
    }
}