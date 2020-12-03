using System;
using Octokit;

namespace GithubRepositoryModel
{
    public interface ICommitInfo
    {
        DateTimeOffset LastCommitted { get; }
        Commit LastCommit { get; }
    }
}