using System.IO;
using System.Linq;
using Emma.Core.Extensions;
using Emma.Core.Github;
using Emma.Core.Tests.Support;
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
        public void Can_parse_MethodInfo()
        {
            var type = typeof(SampleExtensionsClass);

            var extensionMethodInfos = type
                .ExtensionMethods()
                .Select(m => ExtensionMethodParser.Parse(m, new FileInfo(type.Assembly.Location).LastWriteTimeUtc))
                .ToList();

            ConsoleX.Dump(extensionMethodInfos, 
                $"From type: {type.FullName}");
        }

        [Test]
        public void Can_parse_Assembly()
        {
            var type = typeof(SampleExtensionsClass);
            
            ConsoleX.Dump(ExtensionMethodParser.Parse(type.Assembly), 
                $"From Assembly: {type.Assembly.Location}");
        }

        [Test]
        public void Can_parse_source_code()
        {
            var path = @"..\..\..\Support\SampleExtensionsClass.cs";
            var source = File.ReadAllText(path);
            var fullPath = Path.GetFullPath(path);
            var updated = new FileInfo(fullPath).LastWriteTimeUtc;

            ConsoleX.Dump(ExtensionMethodParser.Parse(source, fullPath, updated)
            , $"From: {fullPath}");

        }

        [Test, Explicit("Hits the github api, use for debugging/development purposes")]
        public void Can_parse_github_repo()
        {
            var user = "chrislee187";
            var repo = "Emma";
            var loc = new GithubLocation(user, repo);
            var gitRoot = new Folder(Credentials.AppKey(), loc);
            var extensionsFromFolderInGit = ExtensionMethodParser
                .Parse(gitRoot)
                .OrderBy(i => i.ToString())
                .ToArray();

            var extensionsFromAssemblies = 
                ExtensionMethodParser.Parse(typeof(ReflectionExtensions).Assembly)
                .Concat(ExtensionMethodParser.Parse(typeof(SampleExtensionsClass).Assembly))
                .OrderBy(i => i.ToString())
                .ToArray();

            ConsoleX.Dump(extensionsFromFolderInGit, $"From Git: {loc.ToUrl()}");
            ConsoleX.Dump(extensionsFromAssemblies, $"From Assembly {typeof(ReflectionExtensions).Assembly.Location}");

            extensionsFromFolderInGit.Length
                .ShouldBe(extensionsFromAssemblies.Length);


        }

    }
}