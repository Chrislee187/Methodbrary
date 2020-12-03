using GithubRepositoryModel;
using Octokit;


namespace GithubRepositoryModel.Tests.GithubRepoModel
{
    public class GithubTestsBase
    {
        protected readonly GitHubClient ApiClient;
        protected IGithub Github;

        public GithubTestsBase()
        {
            ApiClient = new GitHubClient(new ProductHeaderValue("EmmaTests"))
            {
                Connection = { Credentials = new Octokit.Credentials(Credentials.AppKey()) }
            };

            Github = new Github(ApiClient);
        }
    }
}