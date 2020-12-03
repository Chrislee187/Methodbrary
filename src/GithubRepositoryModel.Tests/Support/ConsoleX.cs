using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GithubRepositoryModel;

namespace GithubRepositoryModel.Tests.Support
{
    public static class ConsoleX
    {
        private static int _folderDepth;

        public static async Task DumpGithubFolder(GhFolder repo)
        {
            _folderDepth++;
            var ghFolders = await repo.Folders();

            foreach (var folder in ghFolders)
            {
                Console.WriteLine($"{new string('>', _folderDepth)}{folder.Path}");
                foreach (var file in await folder.Files())
                {

                    Console.WriteLine($"{new string(' ', _folderDepth)}{file.Path} : {file.Size} content length");
                }
                await DumpGithubFolder(folder);


            }
            _folderDepth--;
        }

    }
}