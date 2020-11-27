using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Emma.Core
{
    public static class ExtensionMethodParser
    {
        public static ExtensionMethod Parse(MethodInfo mi)
        {
            var prms = mi.GetParameters();

            var paramTypes = prms.Length > 1 
                ? prms[1..].Select(pi => pi.ParameterType).ToArray() 
                : new Type[] { };

            return new ExtensionMethod(mi.Name, 
                prms[0].ParameterType.Name, 
                mi.ReturnType.Name, 
                paramTypes.Select(pt => pt.Name).ToArray());
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
                                prms
                            );

                            ems.Add(em);
                        }
                        break;
                }
            }

            return ems.ToArray();
        }
        //
        // private static ExtensionMethod[] ParseMembers(SyntaxList<SyntaxNode> members)
        // {
        //     var result = new SyntaxList<SyntaxNode>();
        //     foreach (var memberSyntax in members)
        //     {
        //         switch (memberSyntax.Kind())
        //         {
        //             case SyntaxKind.NamespaceDeclaration:
        //                 var nextMembers = ((Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax)memberSyntax)
        //                     .Members;
        //                 result.AddRange(nextMembers);
        //                 break;
        //         }
        //     }
        //     return result;
        // }
        // private static ExtensionMethod[] ParseSyntax(SyntaxNode syntax)
        // {
        //     switch (syntax.Kind())
        //     {
        //         case SyntaxKind.NamespaceDeclaration:
        //             var members = ((Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax)syntax)
        //                 .Members;
        //             break;
        //     }
        //
        // }
    }
}