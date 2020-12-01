using System;
using Emma.Core.Cache;
using Emma.Core.MethodSources;
using Emma.Core.Tests.Github;
using Emma.Core.Tests.Support;
using NUnit.Framework;

namespace Emma.Core.Tests
{
    public class Spikes : GithubTestsBase
    {
        private ExtensionMethodLibrary _library;

        [SetUp]
        public void Setup()
        {
            var source = new GithubRepoExtensionMethodsSource(Github,
                "github.emma.spike.Methodbrary", "chrislee187", "Emma", 
                new AppDataExtensionMethodJsonCache());
            _library = new ExtensionMethodLibrary(new ExtensionMethodsSource[] { source });

        }

        [Test, Explicit]
        public void Spike()
        {

            ConsoleX.Dump(_library.Methods);
        }

        [Test, Explicit]
        public void Spike2()
        {
            var user = "chrislee187";
            var repos = ApiClient.Repository.GetAllForUser(user).Result;
            foreach (var repo in repos)
            {
                var repoContent = ApiClient.Repository.Content.GetAllContents(user, repo.Name).Result;
                Console.WriteLine($"Repo: {repo.Name}");
                foreach (var c in repoContent)
                {
                    Console.WriteLine($"{c.Name} : {c.Content} - {c.Type}");
                }
            }
        }
    }
}