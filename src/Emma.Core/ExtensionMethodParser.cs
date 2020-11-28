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
        public static ExtensionMethod Parse(MethodInfo mi)
        {
            if(!mi.IsStatic) throw new MethodAccessException($"Method '{mi.Name}' is not an extension method");
            var prms = mi.GetParameters();

            if(prms.Length < 1) throw new MethodAccessException($"Method '{mi.Name}' is not an extension method");

            var extendingTypeName = prms[0].ParameterType.Name;
            var paramTypeNames = prms[1..].Select(pi => pi.ParameterType.Name).ToArray();

            var returnTypeName = mi.ReturnType.Name;
            if (mi.ReturnType.IsGenericType)
            {
                returnTypeName = RationaliseGenericTypename(mi);
            }

            return new ExtensionMethod(mi.Name,
                extendingTypeName, 
                returnTypeName, 
                paramTypeNames,
                ExtensionMethodSourceType.Assembly,
                mi.DeclaringType.FullName
                );
        }

        public static ExtensionMethod[] Parse(Assembly asm)
        {
            return asm
                .ExtensionMethods()
                .Select(Parse)
                .ToArray();
        }

        public static ExtensionMethod[] Parse(string sourceCode)
        {
            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = tree.GetCompilationUnitRoot();
            var methods = ParseRoot(root.Members);
            return methods;
        }

        public static ExtensionMethod[] Parse(GithubFolderContent repo)
        {
            return ParseGithubFolder(repo).ToArray();
        }

        private static IEnumerable<ExtensionMethod> ParseGithubFolder(GithubFolderContent folder)
        {
            var list = new List<ExtensionMethod>();

            foreach (var ghFolder in folder.Folders)
            {
                list.AddRange(ParseGithubFolder(ghFolder));
            }

            list.AddRange(ParseGithubFiles(folder));

            return list;
        }

        private static IEnumerable<ExtensionMethod> ParseGithubFiles(GithubFolderContent folder)
        {
            var list = new List<ExtensionMethod>();

            foreach (var file in folder.Files)
            {
                if (Path.GetExtension(file.Name)
                    ?.ToLowerInvariant() == ".cs")
                {
                    var text = file.Content;
                    list.AddRange(ExtensionMethodParser.Parse(text));
                }
            }

            return list;
        }

        private static ExtensionMethod[] ParseRoot(SyntaxList<MemberDeclarationSyntax> members)
        {
            var ems = new List<ExtensionMethod>();
            foreach (var memberSyntax in members)
            {
                switch (memberSyntax.Kind())
                {
                    case SyntaxKind.NamespaceDeclaration:
                        ems.AddRange(ParseRoot(((NamespaceDeclarationSyntax)memberSyntax).Members));
                        break;
                    case SyntaxKind.ClassDeclaration:
                        var classDeclarationSyntax = (ClassDeclarationSyntax)memberSyntax;
                        if (classDeclarationSyntax.IsStatic())
                        {
                            ems.AddRange(ParseRoot(classDeclarationSyntax.Members));
                        }
                        break;
                    case SyntaxKind.MethodDeclaration:
                        var method = (MethodDeclarationSyntax)memberSyntax;

                        if (method.IsExtensionMethod())
                        {
                            var extendingType = method.ParameterList.Parameters.First().Type.Name();
                            var returnType = method.ReturnType.Name();

                            var prms = method.ParameterList.Parameters
                                .Skip(1)
                                .Select(p => p.Type?.Name())
                                .ToArray();

                            var em = new ExtensionMethod(
                                method.Name(),
                                extendingType,
                                returnType,
                                prms,
                                ExtensionMethodSourceType.SourceCode, method.ToString()
                            );

                            ems.Add(em);
                        }
                        break;
                }
            }

            return ems.ToArray();
        }

        private static string RationaliseGenericTypename(MethodInfo mi)
        {
            var returnTypeName = mi.ReturnType.Name;

            var genericTypes = mi.ReturnType.GenericTypeArguments.Select(a => a.Name);
            var genericArgs = string.Join(",", genericTypes);
            returnTypeName = returnTypeName.Substring(0, returnTypeName.IndexOf("`", StringComparison.Ordinal));
            returnTypeName += $"<{genericArgs}>";
            return returnTypeName;
        }
    }

    public enum ExtensionMethodSourceType
    {
        Assembly, SourceCode
    }
}