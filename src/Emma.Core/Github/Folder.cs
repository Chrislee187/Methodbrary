using System;
using System.Collections.Generic;
using Emma.Core.Github.Extensions;
using Octokit;

namespace Emma.Core.Github
{
    public class Folder : IGithubLocation, ICommitInfo
    {
        public string User { get; }
        public string Repo { get; }
        public string Path { get; }

        private Commit _lastCommit;
        public Commit LastCommit => 
            _lastCommit ??= _github.Repository.Commit.LastCommit(this);
        public DateTimeOffset LastCommitted => LastCommit.Committer.Date;

        private readonly List<FileContent> _files = new List<FileContent>();
        public IEnumerable<FileContent> Files
        {
            get
            {
                if(!_folderReadFromGit) RequestFolderInfo();

                return _files;
            }
        }

        private readonly List<Folder> _folders = new List<Folder>();
        public IEnumerable<Folder> Folders
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

        private Folder(IGitHubClient github, IGithubLocation loc, RepositoryContent rc)
        {
            User = loc.User;
            Repo = loc.Repo;
            Path = rc.Path;

            _github = github;
        }

        public Folder(string githubAppKey, IGithubLocation location)
        {
            User = location.User;
            Repo = location.Repo;
            Path = location.Path;

            _github = new GitHubClient(new ProductHeaderValue("Emma"))
            {
                Connection = { Credentials = new Octokit.Credentials(githubAppKey) }
            };
        }
        public Folder(IGitHubClient github, IGithubLocation location)
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
                        _files.Add(new FileContent(_github, new GithubLocation(User, Repo, content.Path), content));
                        break;
                    case ContentType.Dir:
                        _folders.Add(new Folder(_github, this, content));
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
                // SetBaseParams(contents[0]);
            }
            else
            {
                contents = repoContent.GetAllContents(User, Repo, Path)
                    .Result;
            }

            return contents;
        }
    }
}