using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Emma.Core.Tests.Github;
using Emma.Core.Tests.Support;
using NUnit.Framework;

namespace Emma.Core.Tests
{
    public class GithubModelSpikes : GithubTestsBase
    {
        private const string Username = "chrislee187";
        private const string Repo = "Emma";
        private Core.Github.Github _github;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _github = new Core.Github.Github(ApiClient);
        }


        [Test]
        public async Task GetAGithubUser()
        {
            var sw = new Stopwatch();
            sw.Start();
            var user = await _github.User(Username);

            Console.WriteLine($"Github User: {user.Login} ({user.Name}) #{user.PublicRepos} public repos");
            foreach (var repo in await user.Repos())
            {
                Console.WriteLine($"{repo.Name} - {repo.DefaultBranch} - {repo.PushedAt}");
            }

            Console.WriteLine(sw.Elapsed);
            sw.Restart();

            foreach (var repo in await user.Repos())
            {
                Console.WriteLine($"{repo.Name} - {repo.DefaultBranch} - {repo.PushedAt}");
            }
            Console.WriteLine(sw.Elapsed);
        }

        [Test, Explicit]
        public async Task GetARepo()
        {
            var repo = await _github.Repo(Username, Repo);

            Console.WriteLine($"Repo: {repo.Name}");

            foreach (var branch in await repo.Branch())
            {
                Console.WriteLine(branch.Name);
                Console.WriteLine(branch.Root.Path);
                await ConsoleX.DumpGithubFolder(branch.Root);
            }
        }
    }
}
