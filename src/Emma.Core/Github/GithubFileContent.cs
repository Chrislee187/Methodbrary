using System;
using Octokit;

namespace Emma.Core.Github
{
    public class GithubFileContent : RepositoryContent, IGithubLocation
    {
        public string User { get; }
        public string Repo { get; }

        private readonly Func<string> _contentProvider;

        public GithubFileContent(IGitHubClient github, IGithubLocation loc, RepositoryContent rc) : base(rc.Name, rc.Path, rc.Sha, rc.Size, rc.Type.Value, rc.DownloadUrl,
            rc.Url, rc.GitUrl, rc.HtmlUrl, rc.Encoding,
            rc.EncodedContent, rc.Target, rc.SubmoduleGitUrl)
        {
            User = loc.User;
            Repo = loc.Repo;
            Path = loc.Path ?? rc.Path;
            _contentProvider = () => github.Repository.Content.GetAllContents(User, Repo, Path)
                .Result[0]
                .Content;
        }
        private string _content;

        public new string Content => _content ??= _contentProvider() ?? "";
    }
}