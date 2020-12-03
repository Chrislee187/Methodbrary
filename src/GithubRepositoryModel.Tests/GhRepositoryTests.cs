using System;
using System.Threading.Tasks;
using GithubRepositoryModel.Tests.GithubRepoModel;
using GithubRepositoryModel.Tests.Support;
using NUnit.Framework;
using Shouldly;

namespace GithubRepositoryModel.Tests
{
    public class GhRepositoryTests : GithubTestsBase
    {
        private const string Login = "chrislee187";
        private const string RepoName = "Emma";
        private const string DefaultBranchName = "main";
        private IGhRepository _repo;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _repo = await Github.Repository(Login, RepoName);
        }

        [TestCase(RepoName)]
        public void Can_get_Repository(string repoName)
        {
            _repo.Name.ShouldBe(RepoName);
            _repo.PushedAt.HasValue.ShouldBeTrue();
            _repo.PushedAt.Value.ShouldBeGreaterThan(DateTimeOffset.MinValue);
        }
        [Test]
        public async Task Can_get_default_branch()
        {
            var branch = await _repo.GetBranch(_repo.DefaultBranch);

            branch.Name.ShouldBe(DefaultBranchName);
        }

    }
}
