using System.Collections.Generic;
using System.Threading.Tasks;

namespace GithubRepositoryModel
{
    public interface IGhUser
    {
        Task<IEnumerable<IGhRepository>> Repos();
        Task<IGhRepository> Repos(string repoName);
        string Login { get; }
        string Name { get; }
        int PublicRepos { get; }

        // TODO: Add remaining members from User

    }
}