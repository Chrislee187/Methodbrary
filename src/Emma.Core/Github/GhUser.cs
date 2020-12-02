using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace Emma.Core.Github
{
    public class GhUser : User, IGhUser
    {
        private readonly IGithub _gitHub;

        private GhUser(IGithub gitHub, User u) : this(u)
        {
            _gitHub = gitHub;
        }

        public async Task<IEnumerable<IGhRepository>> Repos() => 
            await GhRepository.All(_gitHub, Login);

        public async Task<IGhRepository> Repos(string repoName) => 
            await GhRepository.Get(_gitHub, Login, repoName);

        private GhUser(User u) : base(u.AvatarUrl, u.Bio, u.Blog, u.Collaborators ?? 0, u.Company, u.CreatedAt, u.UpdatedAt,
            u.DiskUsage ?? 0, u.Email, u.Followers, u.Following, u.Hireable, u.HtmlUrl, u.TotalPrivateRepos, u.Id, u.Location,
            u.Login, u.Name, u.NodeId, u.OwnedPrivateRepos, u.Plan, u.PrivateGists ?? 0, u.PublicGists, u.PublicRepos, u.Url, u.Permissions,
            u.SiteAdmin, u.LdapDistinguishedName, u.SuspendedAt)
        {
        }

        #region Api Helpers
        public static async Task<IGhUser> Get(IGithub github, string userName)
        {
            var (user, _)= await GhLogging.LogAsyncTask(() => 
                github.ApiClient.User.Get(userName),
                    $"{nameof(Github)}.{nameof(GhUser)}.{nameof(Get)}");

            return new GhUser(github, user);
        }

        #endregion
    }

}