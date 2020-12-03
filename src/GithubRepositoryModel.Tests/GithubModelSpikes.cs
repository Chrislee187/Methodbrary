using System.Threading.Tasks;
using GithubRepositoryModel.Tests.GithubRepoModel;
using NUnit.Framework;

namespace GithubRepositoryModel.Tests
{
    public class GithubModelSpikes : GithubTestsBase
    {
        private Task<IGhRepository> _repository;
        private const string Login = "chrislee187";

        [Test]
        public void Spike()
        {
            _repository = Github.Repository(Login, "Emma");
        }
    }
}
