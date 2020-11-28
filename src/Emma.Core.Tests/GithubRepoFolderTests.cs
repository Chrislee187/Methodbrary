using System;
using System.Linq;
using Emma.Core.Github;
using NUnit.Framework;
using Shouldly;

namespace Emma.Core.Tests
{
    [TestFixture, Explicit]
    public class GithubRepoFolderTests : GithubTestsBase
    {

        [SetUp]
        public void SetUp()
        {

        }
        [Test, Explicit("Hits the github api,use for debugging/development purposes")]
        public void Can_read_github_folder()
        {
            var repo = new GithubFolderContent(GithubClient, new GithubLocation("ChrisLee187", "Emma"));
            var itemCount = (repo.Folders.Count() + repo.Files.Count());
            itemCount.ShouldBeGreaterThan(0);

            DumpRepo(repo);
        }

        private int _folderDepth;

        private void DumpRepo(GithubFolderContent repo)
        {
            _folderDepth++;
            foreach (var folder in repo.Folders)
            {
                Console.WriteLine($"{new string('>', _folderDepth)}{folder.Path}");
                foreach (var file in folder.Files)
                {

                    Console.WriteLine($"{new string(' ', _folderDepth)}{file.Path} : {file.Size} content length");
                }
                DumpRepo(folder);


            }
            _folderDepth--;

        }

    }
}