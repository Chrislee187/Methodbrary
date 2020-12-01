using Octokit;

namespace Emma.Core.Tests.Github
{
    public class GithubTestsBase
    {
        protected readonly GitHubClient ApiClient;
        protected Core.Github.Github Github;

        public GithubTestsBase()
        {
            ApiClient = new GitHubClient(new ProductHeaderValue("EmmaTests"))
            {
                Connection = { Credentials = new Credentials( Emma.Core.Github.Credentials.AppKey()) }
            };

            Github = new Core.Github.Github(ApiClient);
        }
    }
}