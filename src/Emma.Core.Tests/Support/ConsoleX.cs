using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GithubRepositoryModel;

namespace Emma.Core.Tests.Support
{
    public static class ConsoleX
    {
        private static int _folderDepth;

        public static async Task DumpGithubFolder(GhFolder repo)
        {
            _folderDepth++;
            var ghFolders = await repo.GetFolders();

            foreach (var folder in ghFolders)
            {
                Console.WriteLine($"{new string('>', _folderDepth)}{folder.Path}");
                foreach (var file in await folder.GetFiles())
                {

                    Console.WriteLine($"{new string(' ', _folderDepth)}{file.Path} : {file.Size} content length");
                }
                await DumpGithubFolder(folder);


            }
            _folderDepth--;
        }
        public static void Dump(IEnumerable<ExtensionMethod> methods, string source = null)
        {
            Console.WriteLine();
            if (!string.IsNullOrEmpty(source))
            {
                Console.WriteLine(source);
            }
            foreach (var mi in methods)
            {
                Dump(mi);
            }
        }
        public static void Dump(ExtensionMethod method)
        {
        Console.WriteLine($"{method}");
        }
    }
}