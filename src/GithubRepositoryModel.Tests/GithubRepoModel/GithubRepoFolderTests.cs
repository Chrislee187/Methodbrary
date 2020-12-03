using System;
using System.Linq;
using System.Threading.Tasks;
using GithubRepositoryModel.Tests.Support;
using NUnit.Framework;
using Shouldly;

namespace GithubRepositoryModel.Tests.GithubRepoModel
{
    [TestFixture]
    //[Explicit("Hits the github api,use for debugging/development purposes")]
    public class GithubTests : GithubTestsBase
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task Can_read_github_folder()
        {
            var repo = await Github.Repository("chrislee187", "Emma");
            var defaultBranch = await repo.Branch(repo.DefaultBranch);
            var itemCount = (await defaultBranch.Root.Folders()).Count()
                            + (await defaultBranch.Root.Files()).Count();
            itemCount.ShouldBeGreaterThan(0);

            await ConsoleX.DumpGithubFolder(defaultBranch.Root);
        }

        [Test]
        public async Task Can_read_github_file_contents()
        {
            var repo = await Github.Repository("chrislee187", "Emma");
            var defaultBranch = await repo.Branch(repo.DefaultBranch);
            var gitIgnoreFile = (await defaultBranch.Root.Files()).First(f => f.Path == ".gitignore");

            gitIgnoreFile.Content.ShouldNotBeEmpty();
            Console.WriteLine(gitIgnoreFile.Content);
        }
    }
}