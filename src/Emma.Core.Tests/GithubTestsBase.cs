using Octokit;

namespace Emma.Core.Tests
{
    public class GithubTestsBase
    {
        protected readonly GitHubClient GithubClient;
        public GithubTestsBase()
        {
            GithubClient = new GitHubClient(new ProductHeaderValue("EmmaTests"))
            {
                Connection = { Credentials = new Octokit.Credentials(Credentials.AppKey()) }
            };
        }
    }
}