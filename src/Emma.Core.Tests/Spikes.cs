using System;
using System.IO;
using Emma.Core.Extensions;
using Emma.Core.Github;
using Newtonsoft.Json;
using NUnit.Framework;
using Octokit;

namespace Emma.Core.Tests
{
    public class Spikes : GithubTestsBase
    {
        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public void Spike()
        {
            var emLibrary = new GithubRepoExtensionMethodSource(GithubClient, 
                new GithubLocation("chrislee187", "emma"), 
                new AppDataExtensionMethodCache(),
                "emma.spike" );

        }
    }

    public class GithubRepoExtensionMethodSource : ExtensionMethodSource
    {
        private ExtensionMethod[] _extensionMethods;
        private GitHubClient _github;
        private GithubLocation _location;
        private ExtensionMethodCache _cache;

        public GithubRepoExtensionMethodSource(GitHubClient github, GithubLocation location, ExtensionMethodCache cache, string localCacheId)
        {
            _cache = cache;
            _location = location;
            _github = github;
            if (!_cache.Contains(localCacheId))
            {
                InitCache(localCacheId);
            }
        }

        private void InitCache(string cacheId)
        {
                _cache.Remove(cacheId);
                var root = new GithubFolderContent(_github, _location);
                var extensionMethods = ExtensionMethodParser.Parse(root);
                _cache.Add(cacheId, extensionMethods);
        }


    }

    internal class AppDataExtensionMethodCache : ExtensionMethodCache
    {
        public override bool Contains(string cacheId) =>
            File.Exists(CacheFilename(cacheId));

        public override ExtensionMethod[] Get(string cacheId)
        {
            if (!Contains(cacheId))
            {
                throw new ArgumentException($"CacheId not found!", nameof(cacheId));
            }

            var data = File.ReadAllText(CacheFilename(cacheId));
            return JsonConvert.DeserializeObject<SerializedMethods>(data).Methods;
        }

        public override void Remove(string cacheId)
        {
            if (Contains(cacheId))
            {
                File.Delete(CacheFilename(cacheId));
            }
        }

        public override void Add(string cacheId, ExtensionMethod[] extensionMethods)
        {
            var serialize = new SerializedMethods {Methods = extensionMethods};
            var data = JsonConvert.SerializeObject(serialize, Formatting.None);
            File.WriteAllText(CacheFilename(cacheId), data);
        }
        
        private string CacheFilename(string cacheId)
        {
            var file = cacheId.ToValidFilename();
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Emma");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var full = Path.ChangeExtension(Path.Combine(path,file), "emmaCache");

            return full;
        }

        private class SerializedMethods
        {
            public ExtensionMethod[] Methods;
        }
    }

    public abstract class ExtensionMethodCache
    {
        public abstract bool Contains(string cacheId);
        public abstract ExtensionMethod[] Get(string cacheId);

        public abstract void Remove(string cacheId);

        public abstract void Add(string cacheId, ExtensionMethod[] extensionMethods);
    }

    public class ExtensionMethodSource
    {

    }

    public class ExtensionMethodLibrary
    {
    }
}