using System.Threading.Tasks;
using GithubRepositoryModel.Tests.GithubRepoModel;
using NUnit.Framework;
using Shouldly;

namespace GithubRepositoryModel.Tests
{
    public class GhBranchTests : GithubTestsBase
    {
        private const string Login = "chrislee187";
        private const string RepoName = "Emma";

        private IGhRepository _repo;
        private IGhBranch _branch;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _repo = await Github.Repository(Login, RepoName);
            _branch = await _repo.GetBranch(_repo.DefaultBranch);
        }

        [Test]
        public void Can_get_Root_of_branch()
        {
            var branchRoot = _branch.Root;

            branchRoot.Path.ShouldBeNull();

        }
    }
}
