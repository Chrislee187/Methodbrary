using System;
using Octokit;

namespace Emma.Core.Github
{
    public class GithubFileContent : RepositoryContent
    {
        private readonly Func<string> _contentProvider;

        public GithubFileContent(RepositoryContent rc, GitHubClient github, string user, string repo, string path
        ) : base(rc.Name, rc.Path, rc.Sha,rc.Size, rc.Type.Value, rc.DownloadUrl, rc.Url, rc.GitUrl, rc.HtmlUrl, rc.Encoding, rc.EncodedContent, rc.Target, rc.SubmoduleGitUrl )
        {
            _contentProvider = () => github.Repository.Content.GetAllContents(user, repo, path)
                .Result[0]
                .Content;
        }

        private string _content;

        public new string Content
        {
            get
            {
                if (_content == null)
                {
                    _content = _contentProvider();
                    if(_content == null)
                    {
                        _content = "";
                    }

                }
                return _content;
            }
        }
    }
}