using System.Linq;
using Octokit;

namespace Emma.Core.Github.Extensions
{
    public static class RepositoriesCommitClientExtensions
    {
        public static Commit LastCommit(this IRepositoryCommitsClient commits, IGithubLocation location)
        {
            var lastCommit = commits
                .GetAll(location.User, location.Repo, CommitRequestFilter.ByPath(location.Path))
                .Result.First();
            return lastCommit.Commit;
        }
    }
}