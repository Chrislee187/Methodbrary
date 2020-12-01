using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Emma.Core.Extensions
{
    public static class MemberDeclarationSyntaxExtensions
    {
        public static bool IsStatic(this MemberDeclarationSyntax syntax)
            => syntax.Modifiers.Any(m => m.Kind() == SyntaxKind.StaticKeyword);

        public static bool IsPublic(this MemberDeclarationSyntax syntax)
            => syntax.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword);

    }
}