using System.Linq;
using Emma.Core.Github;
using NUnit.Framework;
using Shouldly;

namespace Emma.Core.Tests
{
    [TestFixture, Explicit]
    public class GithubRepoFolderTests : GithubTestsBase
    {

        [SetUp]
        public void SetUp()
        {

        }
        [Test, Explicit("Hits the github api,use for debugging/development purposes")]
        public void Can_read_github_folder()
        {
            var repo = new Folder(GithubClient, new GithubLocation("ChrisLee187", "Emma"));
            var itemCount = (repo.Folders.Count() + repo.Files.Count());
            itemCount.ShouldBeGreaterThan(0);

            ConsoleX.Dump(repo);
        }
    }
}