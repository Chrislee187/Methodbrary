using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace Emma.Core.Github
{
    public class GhFile : RepositoryContent
    {
        private readonly IGithub _github;
        public readonly GhRepository Repository;
        public readonly GhBranch Branch;

        private GitHubCommit _lastCommit;
        private readonly Func<GitHubCommit> _lastCommitProvider;
        public GitHubCommit LastCommit => _lastCommit ??= _lastCommitProvider() ?? new GitHubCommit();

        private string _content;

        public new string Content
        {
            get
            {
                // TODO: Octokit Content field wasnt always populated,
                // couldn't work out why, further investigation needed
                // simple override here that explicitly get's the file contents
                if (string.IsNullOrEmpty(_content))
                {
                    if (string.IsNullOrEmpty(base.Content))
                    {
                        _content = GetContent(_github, Repository, Branch, Path)
                            .Result.Content;
                    }
                    else
                    {
                        _content = base.Content;
                    }
                }

                return _content ?? "";
            }
        }

        public GhFile(IGithub github, GhRepository repository, GhBranch branch, RepositoryContent content) 
            : this(content)
        {
            _github = github;
            Repository = repository;
            Branch = branch;

  
            _lastCommitProvider = () => _github.ApiClient.Repository
                .Commit.GetAll(Repository.Id, CommitRequestFilter.ByPath(Path)).Result.FirstOrDefault();
        }

        private GhFile(RepositoryContent rc) : base(rc.Name, rc.Path, rc.Sha, rc.Size, rc.Type.Value, rc.DownloadUrl,
            rc.Url, rc.GitUrl, rc.HtmlUrl, rc.Encoding,
            rc.EncodedContent, rc.Target, rc.SubmoduleGitUrl)
        { }

        #region Api Helpers
        public static async Task<RepositoryContent> GetContent(
            IGithub github,
            GhRepository repository,
            GhBranch branch,
            string path)
            => (await github.ApiClient.Repository.Content
                .GetAllContentsByRef(repository.Id, path, branch.Name))[0];

        #endregion
    }
}