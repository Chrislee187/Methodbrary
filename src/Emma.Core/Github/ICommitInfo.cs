using System;
using Octokit;

namespace Emma.Core.Github
{
    public interface ICommitInfo
    {
        DateTimeOffset LastCommitted { get; }
        Commit LastCommit { get; }
    }
}