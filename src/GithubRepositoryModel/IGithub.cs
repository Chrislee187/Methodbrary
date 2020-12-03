using System.Threading.Tasks;
using Octokit;

namespace GithubRepositoryModel
{
    public interface IGithub
    {
        IGitHubClient ApiClient { get; }
        Task<IGhUser> User(string user);
        Task<IGhRepository> Repository(string userName, string repoName);
        Task<IGhRepository> Repository(string url);
    }
}