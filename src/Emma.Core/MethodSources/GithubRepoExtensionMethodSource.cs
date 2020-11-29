using Emma.Core.Cache;
using Emma.Core.Github;
using Octokit;

namespace Emma.Core.MethodSources
{
    public class GithubRepoExtensionMethodsSource : ExtensionMethodsSource
    {
        private readonly GitHubClient _github;
        private readonly GithubLocation _location;
        private readonly ExtensionMethodCache _cache;

        public GithubRepoExtensionMethodsSource(GitHubClient github, GithubLocation location, ExtensionMethodCache cache, string localCacheId)
        {
            _cache = cache;
            _location = location;
            _github = github;

            InitCache(localCacheId);
        }

        private void InitCache(string cacheId)
        {
            if (!_cache.Contains(cacheId))
            {
                GetExtensionMethodsFromGithub();
                _cache.Add(cacheId, this);
            }
            else
            {
                var m = _cache.Get(cacheId);
                var repo = new Folder(_github, _location);
                if (repo.LastCommitted > m.LastUpdated)
                {
                    GetExtensionMethodsFromGithub();
                    _cache.Add(cacheId, this);
                }

                Methods = m.Methods;
                LastUpdated = m.LastUpdated;
            }
        }

        private void GetExtensionMethodsFromGithub()
        {
            var repo = new Folder(_github, _location);

            Methods = ExtensionMethodParser.Parse(repo);
            LastUpdated = repo.LastCommitted;
        }
    }
}