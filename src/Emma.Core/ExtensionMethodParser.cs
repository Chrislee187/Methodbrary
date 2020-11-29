using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Emma.Core.Adapters;
using Emma.Core.Extensions;
using Emma.Core.Github;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Emma.Core
{
    public static class ExtensionMethodParser
    {
        public static ExtensionMethod Parse(MethodInfo mi, DateTime lastUpdated = default) => 
            new MethodInfoExtensionMethod(mi, lastUpdated);

        public static IEnumerable<ExtensionMethod> Parse(IEnumerable<MethodInfo> mis, DateTime lastUpdated = default) => 
            mis.Select(m => Parse(m, lastUpdated));

        public static IEnumerable<ExtensionMethod> Parse(Assembly asm) => 
            Parse(asm.ExtensionMethods(), new FileInfo(asm.Location).LastWriteTimeUtc);

        public static IEnumerable<ExtensionMethod> Parse(string sourceCode, string sourceLocation = default, DateTimeOffset lastUpdated = default) => 
            ParseSyntax(
                CSharpSyntaxTree.ParseText(sourceCode).GetCompilationUnitRoot().Members, 
                sourceLocation, 
                lastUpdated);

        public static IEnumerable<ExtensionMethod> Parse(Folder repo) => 
            ParseGithubFolder(repo).ToArray();

        private static IEnumerable<ExtensionMethod> ParseGithubFolder(Folder folder)
        {
            var list = new List<ExtensionMethod>();

            foreach (var ghFolder in folder.Folders)
            {
                list.AddRange(ParseGithubFolder(ghFolder));
            }

            list.AddRange(ParseGithubFiles(folder.Files));

            return list;
        }

        private static IEnumerable<ExtensionMethod> ParseGithubFiles(IEnumerable<FileContent> fileContents)
        {
            var list = new List<ExtensionMethod>();

            foreach (var file in fileContents)
            {
                if (Path.GetExtension(file.Name)?.ToLowerInvariant() == ".cs")
                {
                    var text = file.Content;
                    list.AddRange(Parse(text, file.Path, file.LastCommitted));
                }
            }

            return list;
        }

        private static IEnumerable<ExtensionMethod> ParseSyntax(
            SyntaxList<MemberDeclarationSyntax> members, 
            string sourceLocation,
            DateTimeOffset lastUpdated) => 
                new MemberSyntaxListExtensionMethods(members, sourceLocation, lastUpdated).ToArray();
    }

    public enum ExtensionMethodSourceType
    {
        Assembly, SourceCode
    }
}