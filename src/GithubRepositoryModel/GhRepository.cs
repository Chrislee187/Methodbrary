using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace GithubRepositoryModel
{
    public class GhRepository : Repository, IGhRepository
    {
        private readonly IGithub _github;

        public GhRepository(IGithub github, Repository c) : this(c)
        {
            _github = github;
            
        }

        // TODO: Commits - github.ApiClient.Repository.Commit.

        public async Task<IGhBranch> GetBranch(string branchName) => 
            await GhBranch.Get(_github, this, branchName ?? DefaultBranch);

        public async Task<IEnumerable<IGhBranch>> GetBranches() => 
            await GhBranch.All(_github, this);
        
        private GhRepository(Repository c) : base(c.Url, c.HtmlUrl, c.CloneUrl, c.GitUrl, c.SshUrl, c.SvnUrl,
            c.MirrorUrl, c.Id, c.NodeId, c.Owner, c.Name, c.FullName, c.IsTemplate, c.Description, c.Homepage, c.Language,
            c.Private, c.Fork, c.ForksCount, c.StargazersCount, c.DefaultBranch, c.OpenIssuesCount, c.PushedAt, c.CreatedAt, c.UpdatedAt,
            c.Permissions, c.Parent, c.Source, c.License, c.HasIssues, c.HasWiki, c.HasDownloads, c.HasPages, c.WatchersCount, c.Size,
            c.AllowRebaseMerge, c.AllowSquashMerge, c.AllowMergeCommit, c.Archived, c.WatchersCount)
        {
        }

        #region Api Helpers
        public static async Task<IGhRepository> Get(IGithub github, string url) => throw new NotImplementedException();
        public static async Task<IGhRepository> Get(IGithub github, string userName, string repoName)
        {
            var (repo, _) = await GhLogging.LogAsyncTask(() =>
                    github.ApiClient.Repository.Get(userName, repoName),
                $"{nameof(Github)}.{nameof(GhRepository)}.{nameof(Get)}");

            return new GhRepository(github, repo);
        }

        public static async Task<IEnumerable<IGhRepository>> All(IGithub github, string userName)
        {
            var (repos, _) = await GhLogging.LogAsyncTask(() =>
                    github.ApiClient.Repository.GetAllForUser(userName),
                $"{nameof(Github)}.{nameof(GhRepository)}.{nameof(All)}");


            return repos
                .Select(r => new GhRepository(github, r));
        }

        #endregion
    }
}