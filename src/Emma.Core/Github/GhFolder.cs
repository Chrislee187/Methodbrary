using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using Octokit;

namespace Emma.Core.Github
{
    public class GhFolder
    {
        private readonly IGithub _github;
        public readonly GhRepository Repository;
        public readonly GhBranch Branch;
        public readonly string Path;

        public bool CacheContent = true;

        private readonly List<GhFile> _files = new List<GhFile>();
        public async Task<IEnumerable<GhFile>> Files()
        {
                await UpdateCachedFolders();

                return _files;
        }

        private readonly List<GhFolder> _folders = new List<GhFolder>();
        public async Task<IEnumerable<GhFolder>> Folders()
        {
                await UpdateCachedFolders();

                return _folders;
        }

        public GhFolder(IGithub github, GhRepository repository, GhBranch branch)
        {
            Repository = repository;
            Branch = branch;

            _github = github;
        }

        private GhFolder(IGithub github, GhRepository repository, GhBranch branch,
            string path)
        {
            Repository = repository;
            Branch = branch;
            Path = path;
            _github = github;
        }

        private bool _folderReadFromGit;

        private async Task UpdateCachedFolders()
        {
            if (_folderReadFromGit && CacheContent) return;
            var contents = await GetContent(_github, Repository, Branch, Path);

            foreach (var content in contents)
            {
                switch (content.Type.Value)
                {
                    case ContentType.File:
                        _files.Add(new GhFile(_github, Repository, Branch, content));
                        break;
                    case ContentType.Dir:
                        _folders.Add(new GhFolder(_github, Repository, Branch, content.Path));
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

        #region Api Helpers

        public static async Task<IEnumerable<RepositoryContent>> GetContent(
            IGithub github, 
            GhRepository repository, 
            GhBranch branch, 
            string path = null)
            => await github.ApiClient.Repository.Content
                .GetAllContentsByRef(repository.Id, path ?? ".", branch.Name);

        #endregion
    }
}