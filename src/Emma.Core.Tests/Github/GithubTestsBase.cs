using Octokit;

namespace Emma.Core.Tests.Github
{
    public class GithubTestsBase
    {
        protected readonly GitHubClient GithubClient;
        public GithubTestsBase()
        {
            GithubClient = new GitHubClient(new ProductHeaderValue("EmmaTests"))
            {
                Connection = { Credentials = new Credentials( Emma.Core.Github.Credentials.AppKey()) }
            };
        }
    }
}