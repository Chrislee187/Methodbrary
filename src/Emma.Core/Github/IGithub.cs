using System.Threading.Tasks;
using Octokit;

namespace Emma.Core.Github
{
    public interface IGithub
    {
        IGitHubClient ApiClient { get; }
        Task<IGhUser> User(string user);
        Task<IGhRepository> Repo(string userName, string repoName);
        Task<IGhRepository> Repo(string url);
    }
}