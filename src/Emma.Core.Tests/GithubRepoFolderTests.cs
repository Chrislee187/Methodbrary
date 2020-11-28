using System;
using Emma.Core.Github;
using NUnit.Framework;

namespace Emma.Core.Tests
{
    [TestFixture, Explicit]
    public class GithubRepoFolderTests
    {

        [Test, Explicit("Hits the github api,use for debugging/development purposes")]
        public void Can_read_github_folder()
        {
            // var repo = new GithubRepoFolder("ChrisLee187", "Emma", Credentials.AppKey());
            var repo = new GithubRepoFolder(Credentials.AppKey(), new GithubLocation("ChrisLee187", "Emma"));

            DumpRepo(repo);
        }

        private int _folderDepth = 0;
        private void DumpRepo(GithubRepoFolder repo)
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