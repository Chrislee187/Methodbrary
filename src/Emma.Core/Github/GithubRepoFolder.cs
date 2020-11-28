using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace Emma.Core.Github
{

    public class GithubRepoFolder : RepositoryContent, IGithubLocation
    {
        public string User { get; }
        public string Repo { get; }
        public new string Path { get; }

        private readonly List<GithubFileContent> _files = new List<GithubFileContent>();
        private readonly List<GithubRepoFolder> _folders = new List<GithubRepoFolder>();

        public IEnumerable<GithubFileContent> Files
        {
            get
            {
                if(!_folderReadFromGit) RequestFolderInfo();

                return _files;
            }
        }

        public IEnumerable<GithubRepoFolder> Folders
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

        private bool _folderReadFromGit = false;
        private readonly IGitHubClient _github;

        private GithubRepoFolder(IGitHubClient github, IGithubLocation loc, RepositoryContent rc) : base(rc.Name, rc.Path, rc.Sha, rc.Size, rc.Type.Value, rc.DownloadUrl,
            rc.Url, rc.GitUrl, rc.HtmlUrl, rc.Encoding,
            rc.EncodedContent, rc.Target, rc.SubmoduleGitUrl)
        {
            User = loc.User;
            Repo = loc.Repo;
            Path = rc.Path;

            _github = github;
        }

        public GithubRepoFolder(string githubAppKey, GithubLocation location)
        {
            User = location.User;
            Repo = location.Repo;
            Path = location.Path;

            _github = new GitHubClient(new ProductHeaderValue("Emma"))
            {
                Connection = { Credentials = new Octokit.Credentials(githubAppKey) }
            };
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
                        _files.Add(new GithubFileContent(_github, this, content));
                        break;
                    case ContentType.Dir:
                        _folders.Add(new GithubRepoFolder(_github, this, content));
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
            base.Path = content
                .Path;
            base.Name = content
                .Name;
            base.Sha = content
                .Sha;
            base.Size = content
                .Size;
            base.Type = content
                .Type;
            base.DownloadUrl = content
                .DownloadUrl;
            base.Url = content
                .Url;
            base.GitUrl = content
                .GitUrl;
            base.HtmlUrl = content
                .HtmlUrl;
            base.Encoding = content
                .Encoding;
            base.Target = content
                .Target;
            base.SubmoduleGitUrl = content
                .SubmoduleGitUrl;
        }
    }
}