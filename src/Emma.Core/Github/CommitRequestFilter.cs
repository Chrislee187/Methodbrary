using Octokit;

namespace Emma.Core.Github
{
    public class CommitRequestFilter
    {
        public static CommitRequest ByPath(string path) => new CommitRequest() {Path = path};

    }
}