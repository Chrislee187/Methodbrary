using System.Threading.Tasks;
using Octokit;

namespace Emma.Core.Github
{
    public interface IGithub
    {
        IGitHubClient ApiClient { get; }
        Task<GhUser> User(string user);
        Task<GhRepository> Repo(string userName, string repoName);
        Task<GhRepository> Repo(string url);
    }
}