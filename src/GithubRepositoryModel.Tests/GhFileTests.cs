using System.Linq;
using System.Threading.Tasks;
using GithubRepositoryModel.Tests.GithubRepoModel;
using NUnit.Framework;
using Shouldly;

namespace GithubRepositoryModel.Tests
{
    public class GhFileTests : GithubTestsBase
    {
        private const string Login = "chrislee187";
        private const string RepoName = "Emma";

        private GhFile _file;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var repo = await Github.Repository(Login, RepoName);
            var branch = await repo.GetBranch(repo.DefaultBranch);
            var folder = branch.Root;
            _file = (await folder.GetFiles()).First();
        }

        [Test]
        public void Can_get_file_content()
        {
            _file.Content.Length.ShouldBeGreaterThan(0);
        }
    }
}
