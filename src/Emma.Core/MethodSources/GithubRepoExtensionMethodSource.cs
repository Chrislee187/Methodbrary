using Emma.Core.Cache;
using Emma.Core.Github;

namespace Emma.Core.MethodSources
{
    public class GithubRepoExtensionMethodsSource : ExtensionMethodsSource
    {

        private readonly ExtensionMethodCache _cache;
        private readonly IGithub _github;
        private readonly string _userName;
        private readonly string _repoName;

        public GithubRepoExtensionMethodsSource(IGithub github, string localCacheId,
            string userName, string repoName,
            ExtensionMethodCache cache)
        {
            _github = github;
            _cache = cache;
            _userName = userName;
            _repoName = repoName;
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
                var ghRepository = _github.User(_userName).Result
                    .Repos(_repoName).Result;
                
                var updateCache = ghRepository.UpdatedAt > m.LastUpdated;
                if (updateCache)
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
            // Note: Had probllems when using the Async calls, because of being called from CTOR
            var user = _github.User(_userName).Result;
            var repo = user.Repos(_repoName).Result;
            var defaultBranch = repo.Branch(repo.DefaultBranch).Result;
            Methods = ExtensionMethodParser.Parse(defaultBranch.Root).Result;
            LastUpdated = repo.UpdatedAt;
        }

    }
}