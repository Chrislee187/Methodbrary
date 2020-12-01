using System;
using System.Linq;
using System.Threading.Tasks;
using Emma.Core.Tests.Support;
using NUnit.Framework;
using Shouldly;

namespace Emma.Core.Tests.Github
{
    [TestFixture, Explicit]
    public class GithubTests : GithubTestsBase
    {

        [SetUp]
        public void SetUp()
        {

        }
        [Test, Explicit("Hits the github api,use for debugging/development purposes")]
        public async Task Can_read_github_folder()
        {
            var repo = await Github.Repo("chrislee187", "Emma");
            var defaultBranch = await repo.Get();
            var itemCount = (await defaultBranch.Root.Folders()).Count() 
                            + (await defaultBranch.Root.Files()).Count();
            itemCount.ShouldBeGreaterThan(0);

            await ConsoleX.DumpGithubFolder(defaultBranch.Root);
        }
        [Test, Explicit("Hits the github api,use for debugging/development purposes")]
        public async Task Can_read_github_file_contents()
        {
            var repo = await Github.Repo("chrislee187", "Emma");
            var defaultBranch = await repo.Get();
            var gitIgnoreFile = (await defaultBranch.Root.Files()).First(f => f.Path == ".gitignore");

            gitIgnoreFile.Content.ShouldNotBeEmpty();
            Console.WriteLine(gitIgnoreFile.Content);
        }
    }
}