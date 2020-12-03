using System.Linq;
using System.Threading.Tasks;
using GithubRepositoryModel.Tests.GithubRepoModel;
using GithubRepositoryModel.Tests.Support;
using NUnit.Framework;
using Shouldly;

namespace GithubRepositoryModel.Tests
{
    public class GhFolderTests : GithubTestsBase
    {
        private const string Login = "chrislee187";
        private const string RepoName = "Emma";

        private GhFolder _folder;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var repo = await Github.Repository(Login, RepoName);
            var branch = await repo.GetBranch(repo.DefaultBranch);
            _folder = branch.Root;
        }

        [Test]
        public async Task Can_get_Root_of_branch()
        {
            _folder.Path.ShouldBeNull();
            var ghFolders = await _folder.GetFolders();
            var ghFiles = await _folder.GetFiles();

            ghFolders.Count().ShouldBeGreaterThan(0);
            ghFiles.Count().ShouldBeGreaterThan(0);

            await ConsoleX.DumpGithubFolder(_folder);
        }
    }
}
