using System;
using System.Threading.Tasks;
using Octokit;

namespace Emma.Core.Github
{
    public class Github : IGithub
    {
        public IGitHubClient ApiClient { get; }

        public Github(IGitHubClient apiClient)
        {
            ApiClient = apiClient;
        }

        public async Task<GhUser> User(string userName) => 
            await GhUser.Get(this, userName);

        public async Task<GhRepository> Repo(string userName, string repoName) => 
            await GhRepository.Get(this, userName, repoName);

        public Task<GhRepository> Repo(string url)
        {
            throw new NotImplementedException();
        }
    }
}