using Emma.Core.Cache;
using Emma.Core.Github;
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
            var emSource = new GithubRepoExtensionMethodsSource(GithubClient, 
                new GithubLocation("chrislee187", "Emma"), 
                new AppDataExtensionMethodJsonCache(),
                "github.emma.spike" );

            ConsoleX.Dump(emSource.Methods);
        }
    }
}