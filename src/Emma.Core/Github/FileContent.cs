using System;
using Emma.Core.Github.Extensions;
using Octokit;

namespace Emma.Core.Github
{
    public class FileContent : RepositoryContent, IGithubLocation, ICommitInfo
    {
        private string _content;
        private readonly IGitHubClient _github;

        public string User { get; }
        public string Repo { get; }

        private readonly Func<RepositoryContent> _contentProvider;

        public new string Content => _content ??= _contentProvider().Content ?? "";
        public DateTimeOffset LastCommitted => LastCommit.Committer.Date;
        private Commit _lastCommit;

        public Commit LastCommit =>
            _lastCommit ??= _github.Repository.Commit.LastCommit(this);

        public FileContent(IGitHubClient github, IGithubLocation loc, RepositoryContent rc) : base(rc.Name, rc.Path, rc.Sha, rc.Size, rc.Type.Value, rc.DownloadUrl,
            rc.Url, rc.GitUrl, rc.HtmlUrl, rc.Encoding,
            rc.EncodedContent, rc.Target, rc.SubmoduleGitUrl)
        {
            _github = github;
            User = loc.User;
            Repo = loc.Repo;
            Path = loc.Path ?? rc.Path;

            _contentProvider = () => _github.Repository.Content.GetAllContents(User, Repo, Path).Result[0];
        }

    }
}