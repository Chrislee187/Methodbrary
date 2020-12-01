using Emma.Core.Cache;
using Emma.Core.MethodSources;
using Emma.Core.Tests.Github;
using Emma.Core.Tests.Support;
using NUnit.Framework;

namespace Emma.Core.Tests
{
    public class GithubRepoExtensionMethodsSourceTest : GithubTestsBase
    {
        [Test]
        public void Initialises()
        {
            var emSource = new GithubRepoExtensionMethodsSource(Github,
                "github.emma.spike", 
                "chrislee187", "Emma", 
                new AppDataExtensionMethodJsonCache());

            ConsoleX.Dump(emSource.Methods);
        }
    }
}