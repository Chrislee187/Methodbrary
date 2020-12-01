using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace Emma.Core.Github
{
    public interface IGhBranch
    {
        GhFolder Root { get; }
    }

    public class GhBranch : Branch, IGhBranch
    {
        private readonly IGithub _github;
        public readonly GhRepository Repository;

        public GhFolder Root { get; }

        private GhBranch(IGithub github, GhRepository repository, Branch branch) 
            : base(branch.Name, branch.Commit, branch.Protected)
        {
            Repository = repository;
            _github = github;
            Root = new GhFolder(_github, Repository, this);
            // var allContentsByRef = _gitHubClient.Repository.Content.GetAllContentsByRef(_repository.Id, Name);
        }

        #region Api Helpers
        public static async Task<GhBranch> Get(IGithub github, GhRepository repository, string branchName)
        {
            return new GhBranch(github, repository,
                await github.ApiClient.Repository.Branch.Get(repository.Id, branchName));
        }

        public static async Task<IEnumerable<GhBranch>> All(IGithub github, GhRepository repository)
        {
            return (await github.ApiClient.Repository.Branch.GetAll(repository.Id))
                .Select(b => new GhBranch(github, repository, b));
        }

        #endregion

    }
}