using System;
using System.Collections;
using System.Collections.Generic;
using Emma.Core.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Emma.Core.Adapters
{
    public class MemberSyntaxListExtensionMethods : IEnumerable<ExtensionMethod>
    {
        private readonly IEnumerable<ExtensionMethod> _methods;

        public MemberSyntaxListExtensionMethods(SyntaxList<MemberDeclarationSyntax> members, 
            string sourceLocation,
            DateTimeOffset lastUpdated)
        {
            _methods = ParseSyntax(members, sourceLocation, lastUpdated);
        }

        private string _lastClassName = "";
        private ExtensionMethod[] ParseSyntax(SyntaxList<MemberDeclarationSyntax> members, 
            string sourceLocation,
            DateTimeOffset lastUpdated)
        {
            var ems = new List<ExtensionMethod>();
            foreach (var memberSyntax in members)
            {
                switch (memberSyntax.Kind())
                {
                    case SyntaxKind.NamespaceDeclaration:
                        ems.AddRange(ParseSyntax(((NamespaceDeclarationSyntax)memberSyntax).Members, 
                            sourceLocation,
                            lastUpdated));
                        break;
                    case SyntaxKind.ClassDeclaration:
                        var classDeclarationSyntax = (ClassDeclarationSyntax)memberSyntax;
                        if (classDeclarationSyntax.IsStatic())
                        {
                            _lastClassName = classDeclarationSyntax.Identifier.Text;
                            ems.AddRange(ParseSyntax(classDeclarationSyntax.Members, sourceLocation, lastUpdated));
                        }

                        break;
                    case SyntaxKind.MethodDeclaration:
                        var method = (MethodDeclarationSyntax)memberSyntax;

                        if (method.IsExtensionMethod())
                        {
                            ems.Add(new MemberSyntaxExtensionMethod(method, lastUpdated, _lastClassName, sourceLocation));
                        }

                        break;
                }
            }

            return ems.ToArray();
        }

        public IEnumerator<ExtensionMethod> GetEnumerator() => _methods.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _methods).GetEnumerator();
    }
}