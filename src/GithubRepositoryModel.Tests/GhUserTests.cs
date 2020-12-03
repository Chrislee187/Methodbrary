using System;
using System.Threading.Tasks;
using GithubRepositoryModel.Tests.GithubRepoModel;
using NUnit.Framework;
using Shouldly;

namespace GithubRepositoryModel.Tests
{
    public class GhUserTests : GithubTestsBase
    {
        private const string Login = "Chrislee187";
        private const string RepoName = "Emma";
        private IGithub _github;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _github = new Github(ApiClient);
        }


        [TestCase(Login)]
        public async Task GetAGithubUser(string loginName)
        {
            var user = await _github.User(loginName);

            user.Login.ShouldBe(loginName);
        }

        [TestCase(RepoName)]
        public async Task Can_get_a_Repository(string repoName)
        {
            var repo = await _github.Repository(Login, repoName);

            repo.Name.ShouldBe(RepoName);
            repo.PushedAt.HasValue.ShouldBeTrue();
            repo.PushedAt.Value.ShouldBeGreaterThan(DateTimeOffset.MinValue);
        }

    }
}
