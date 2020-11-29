using System;
using System.Collections.Generic;
using Emma.Core.Github;

namespace Emma.Core.Tests.Support
{
    public static class ConsoleX
    {
        private static int _folderDepth;

        public static void Dump(Folder repo)
        {
            _folderDepth++;
            foreach (var folder in repo.Folders)
            {
                Console.WriteLine($"{new string('>', _folderDepth)}{folder.Path}");
                foreach (var file in folder.Files)
                {

                    Console.WriteLine($"{new string(' ', _folderDepth)}{file.Path} : {file.Size} content length");
                }
                Dump(folder);


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