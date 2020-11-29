using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Emma.Core.Extensions;
using Emma.Core.Github;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Emma.Core
{
    public static class ExtensionMethodParser
    {
        public static ExtensionMethod Parse(MethodInfo mi, DateTime lastUpdated) => 
            new MethodInfoExtensionMethod(mi, lastUpdated);

        public static IEnumerable<ExtensionMethod> Parse(IEnumerable<MethodInfo> mis, DateTime lastUpdated) => 
            mis.Select(m => ExtensionMethodParser.Parse(m, lastUpdated));

        public static ExtensionMethod[] Parse(Assembly asm) => 
            asm.ExtensionMethods()
            .Select(m => Parse(m, new FileInfo(asm.Location).LastWriteTimeUtc))
            .ToArray();

        public static ExtensionMethod[] Parse(string sourceCode, string sourceLocation, DateTimeOffset lastUpdated) => 
            ParseSyntax(
                CSharpSyntaxTree.ParseText(sourceCode).GetCompilationUnitRoot().Members, 
                sourceLocation, 
                lastUpdated);

        public static ExtensionMethod[] Parse(Folder repo)
        {
            return ParseGithubFolder(repo)
                .ToArray();
        }

        private static IEnumerable<ExtensionMethod> ParseGithubFolder(Folder folder)
        {
            var list = new List<ExtensionMethod>();

            foreach (var ghFolder in folder.Folders)
            {
                list.AddRange(ParseGithubFolder(ghFolder));
            }

            list.AddRange(ParseGithubFiles(folder));

            return list;
        }

        private static IEnumerable<ExtensionMethod> ParseGithubFiles(Folder folder)
        {
            var list = new List<ExtensionMethod>();

            foreach (var file in folder.Files)
            {
                if (Path.GetExtension(file.Name)
                    ?.ToLowerInvariant() == ".cs")
                {
                    var text = file.Content;
                    list.AddRange(ExtensionMethodParser.Parse(text, file.Path, file.LastCommitted));
                }
            }

            return list;
        }

        private static string _lastClassName = "";

        private static ExtensionMethod[] ParseSyntax(SyntaxList<MemberDeclarationSyntax> members, string sourceLocation,
            DateTimeOffset lastUpdated)
        {
            var ems = new List<ExtensionMethod>();
            foreach (var memberSyntax in members)
            {
                switch (memberSyntax.Kind())
                {
                    case SyntaxKind.NamespaceDeclaration:
                        ems.AddRange(ParseSyntax(((NamespaceDeclarationSyntax) memberSyntax).Members, sourceLocation,
                            lastUpdated));
                        break;
                    case SyntaxKind.ClassDeclaration:
                        var classDeclarationSyntax = (ClassDeclarationSyntax) memberSyntax;
                        _lastClassName = classDeclarationSyntax.Identifier.Text;
                        if (classDeclarationSyntax.IsStatic())
                        {
                            ems.AddRange(ParseSyntax(classDeclarationSyntax.Members, sourceLocation, lastUpdated));
                        }

                        break;
                    case SyntaxKind.MethodDeclaration:
                        var method = (MethodDeclarationSyntax) memberSyntax;

                        if (method.IsExtensionMethod())
                        {
                            ems.Add(new MemberSyntaxExtensionMethod(method, lastUpdated, _lastClassName, sourceLocation));
                        }

                        break;
                }
            }

            return ems.ToArray();
        }

    }
    
    public enum ExtensionMethodSourceType
    {
        Assembly, SourceCode
    }
}