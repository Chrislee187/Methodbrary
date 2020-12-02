using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace Emma.Core.Github
{
    public class GhBranch : Branch, IGhBranch
    {
        private readonly IGithub _github;
        public readonly IGhRepository Repository;

        public GhFolder Root { get; }

        private GhBranch(IGithub github, IGhRepository repository, Branch branch) 
            : base(branch.Name, branch.Commit, branch.Protected)
        {
            Repository = repository;
            _github = github;
            Root = new GhFolder(_github, Repository, this);
            // var allContentsByRef = _gitHubClient.Repository.Content.GetAllContentsByRef(_repository.Id, Name);
        }

        #region Api Helpers
        public static async Task<IGhBranch> Get(IGithub github, GhRepository repository, string branchName)
        {
            var (branch, _) = await GhLogging.LogAsyncTask(() =>
                    github.ApiClient.Repository.Branch.Get(repository.Id, branchName),
                $"{nameof(Github)}.{nameof(GhBranch)}.{nameof(Get)}");

            return new GhBranch(github, repository, branch);
        }

        public static async Task<IEnumerable<IGhBranch>> All(IGithub github, GhRepository repository)
        {
            var (all, _) = await GhLogging.LogAsyncTask(() =>
                    github.ApiClient.Repository.Branch.GetAll(repository.Id),
                $"{nameof(Github)}.{nameof(GhBranch)}.{nameof(All)}");

            return all.Select(b => new GhBranch(github, repository, b));
        }

        #endregion

    }
}