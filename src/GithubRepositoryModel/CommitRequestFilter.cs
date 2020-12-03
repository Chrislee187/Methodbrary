using Octokit;

namespace GithubRepositoryModel
{
    public class CommitRequestFilter
    {
        public static CommitRequest ByPath(string path) => new CommitRequest() {Path = path};

    }
}