using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Emma.Core.Extensions;
using Emma.Core.Tests.Support;
using NUnit.Framework;
using Octokit;
using Shouldly;
using Credentials = Emma.Core.Github.Credentials;

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
        public async Task Can_parse_github_repo()
        {
            var client = new GitHubClient(new ProductHeaderValue("Emma"))
            {
                Connection = { Credentials = new Octokit.Credentials(Credentials.AppKey()) }
            };

            var github = new Core.Github.Github(client);


            var username = "chrislee187";
            var reponame = "Emma";
            var user = await github.User(username);
            var repo = await user.Repos(reponame);
            var main = await repo.Get();
            
            var extensionMethods = await ExtensionMethodParser.Parse(main.Root);
            var extensionsFromFolderInGit = extensionMethods
                .Where(em => !em.SourceLocation.Contains("Methodbrary"))
                .OrderBy(i => i.ToString())
                .ToArray();

            var extensionsFromAssemblies = 
                ExtensionMethodParser.Parse(typeof(ReflectionExtensions).Assembly)
                .Concat(ExtensionMethodParser.Parse(typeof(SampleExtensionsClass).Assembly))
                 .OrderBy(i => i.ToString())
                .ToArray();

            ConsoleX.Dump(extensionsFromFolderInGit, $"From Git: {main.Root}");
            ConsoleX.Dump(extensionsFromAssemblies, $"From Assembly {typeof(ReflectionExtensions).Assembly.Location}");

            extensionsFromFolderInGit.Length.ShouldBeGreaterThan(0);
            extensionsFromAssemblies.Length.ShouldBeGreaterThan(0);

            if (extensionsFromAssemblies.Length != extensionsFromFolderInGit.Length)
            {
                Assert.Inconclusive($"Number of methods in Assembly ({extensionsFromAssemblies.Length}) != number of methods in Git folder ({extensionsFromFolderInGit.Length})");
            }


        }

    }
}