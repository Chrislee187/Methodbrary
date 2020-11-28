using System;
using System.Collections.Generic;
using Octokit;

namespace Emma.Core.Github
{

    public class GithubFolderContent : RepositoryContent, IGithubLocation
    {
        public string User { get; }
        public string Repo { get; }

        private readonly List<GithubFileContent> _files = new List<GithubFileContent>();
        private readonly List<GithubFolderContent> _folders = new List<GithubFolderContent>();

        public IEnumerable<GithubFileContent> Files
        {
            get
            {
                if(!_folderReadFromGit) RequestFolderInfo();

                return _files;
            }
        }

        public IEnumerable<GithubFolderContent> Folders
        {
            get
            {
                if (!_folderReadFromGit)
                {
                    RequestFolderInfo();
                }

                return _folders;
            }
        }

        private bool _folderReadFromGit;
        private readonly IGitHubClient _github;

        private GithubFolderContent(IGitHubClient github, IGithubLocation loc, RepositoryContent rc) : base(rc.Name, rc.Path, rc.Sha, rc.Size, rc.Type.Value, rc.DownloadUrl,
            rc.Url, rc.GitUrl, rc.HtmlUrl, rc.Encoding,
            rc.EncodedContent, rc.Target, rc.SubmoduleGitUrl)
        {
            User = loc.User;
            Repo = loc.Repo;
            Path = rc.Path;

            _github = github;
        }

        public GithubFolderContent(string githubAppKey, IGithubLocation location)
        {
            User = location.User;
            Repo = location.Repo;
            Path = location.Path;

            _github = new GitHubClient(new ProductHeaderValue("Emma"))
            {
                Connection = { Credentials = new Octokit.Credentials(githubAppKey) }
            };
        }
        public GithubFolderContent(GitHubClient github, IGithubLocation location)
        {
            User = location.User;
            Repo = location.Repo;
            Path = location.Path;

            _github = github;
        }
        private void RequestFolderInfo()
        {
            var repoContent = _github.Repository.Content;
            var contents = GetFolderContents(repoContent);

            foreach (var content in contents)
            {
                switch (content.Type.Value)
                {
                    case ContentType.File:
                        _files.Add(new GithubFileContent(_github, new GithubLocation(User, Repo, content.Path), content));
                        break;
                    case ContentType.Dir:
                        _folders.Add(new GithubFolderContent(_github, this, content));
                        break;
                    case ContentType.Symlink:
                        break;
                    case ContentType.Submodule:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _folderReadFromGit = true;
        }

        private IEnumerable<RepositoryContent> GetFolderContents(IRepositoryContentsClient repoContent)
        {
            IReadOnlyList<RepositoryContent> contents;
            if (string.IsNullOrEmpty(Path))
            {
                contents = repoContent.GetAllContents(User, Repo)
                    .Result;
                SetBaseParams(contents[0]);
            }
            else
            {
                contents = repoContent.GetAllContents(User, Repo, Path)
                    .Result;
            }

            return contents;
        }

        private void SetBaseParams(RepositoryContent content)
        {
            Path = content.Path;
            Name = content.Name;
            Sha = content.Sha;
            Size = content.Size;
            Type = content.Type;
            DownloadUrl = content.DownloadUrl;
            Url = content.Url;
            GitUrl = content.GitUrl;
            HtmlUrl = content.HtmlUrl;
            Encoding = content.Encoding;
            Target = content.Target;
            SubmoduleGitUrl = content.SubmoduleGitUrl;
        }
    }
}