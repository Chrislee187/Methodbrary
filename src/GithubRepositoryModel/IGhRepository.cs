using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GithubRepositoryModel
{
    public interface IGhRepository
    {
        Task<IGhBranch> Branch(string branchName);
        Task<IEnumerable<IGhBranch>> Branches();

        long Id { get; }

        DateTimeOffset UpdatedAt { get; }
        string DefaultBranch { get; }
        string Name { get; }
        DateTimeOffset? PushedAt { get; }

        // TODO: Add remaining members from Repository
    }
}