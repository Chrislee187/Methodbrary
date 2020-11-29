using Emma.Core.Cache;
using Emma.Core.Github;
using Emma.Core.MethodSources;
using Emma.Core.Tests.Github;
using NUnit.Framework;

namespace Emma.Core.Tests
{
    public class Spikes : GithubTestsBase
    {
        private ExtensionMethodLibrary _library;

        [SetUp]
        public void Setup()
        {
            var source = new GithubRepoExtensionMethodsSource(GithubClient,
                new GithubLocation("chrislee187", "Emma"),
                new AppDataExtensionMethodJsonCache(),
                "github.emma.spike.emma");
            _library = new ExtensionMethodLibrary(new ExtensionMethodsSource[] { source });
        }
    }
}