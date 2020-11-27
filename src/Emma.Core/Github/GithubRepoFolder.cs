using System;
using System.Collections.Generic;
using Octokit;

namespace Emma.Core.Github
{
    public class GithubRepoFolder
    {
        public readonly string User;
        public readonly string Repo;
        public readonly string Path;

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
                if (!_folderReadFromGit) RequestFolderInfo();

                return _folders;
            }
        }

        private bool _folderReadFromGit = false;
        private readonly GitHubClient _github;
        private string _githubAppKey;

        public GithubRepoFolder(string user, string repo, string path, GitHubClient github)
        {
            User = user;
            Repo = repo;
            Path = path;

            _github = github ?? new GitHubClient(new ProductHeaderValue("Emma"))
            {
                Connection = { Credentials = new Octokit.Credentials(Credentials.AppKey()) }
            };
        }
        public GithubRepoFolder(string user, string repo, string githubAppKey, string path = null)
        {
            _githubAppKey = githubAppKey;
            User = user;
            Repo = repo;
            Path = path;

            _github = new GitHubClient(new ProductHeaderValue("Emma"))
            {
                Connection = { Credentials = new Octokit.Credentials(githubAppKey) }
            };
        }
        
        private void RequestFolderInfo()
        {
            var repoContent = _github.Repository.Content;

            var contents = string.IsNullOrEmpty(Path) 
                ? repoContent.GetAllContents(User, Repo).Result 
                : repoContent.GetAllContents(User, Repo, Path).Result;


            foreach (var content in contents)
            {
                switch (content.Type.Value)
                {
                    case ContentType.File:
                        _files.Add(new GithubFileContent(content, _github, User, Repo, content.Path));
                        break;
                    case ContentType.Dir:
                        _folders.Add(new GithubRepoFolder(User, Repo, content.Path, _github));
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
    }
}