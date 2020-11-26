using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CSharpExtensions = Microsoft.CodeAnalysis.CSharp.CSharpExtensions;

namespace Emma.Core
{
    public static class RoslynExtensions
    {
        public static bool IsStatic(this MemberDeclarationSyntax syntax)
            => syntax.Modifiers.Any(m => CSharpExtensions.Kind((SyntaxToken) m) == SyntaxKind.StaticKeyword);

        public static bool IsPublic(this MemberDeclarationSyntax syntax)
            => syntax.Modifiers.Any(m => m.Kind() == SyntaxKind.PublicKeyword);

        public static bool IsThis(this BaseParameterSyntax syntax)
            => syntax.Modifiers.Any(m => m.Kind() == SyntaxKind.ThisKeyword);

        public static bool IsExtensionMethod(this MethodDeclarationSyntax syntax) 
            => syntax.IsPublic() && syntax.IsStatic() 
                                 && (syntax.ParameterList.Parameters.Any() 
                                     && syntax.ParameterList.Parameters.First().IsThis());

        public static string Name(this TypeSyntax? syntax) => syntax?.GetText().ToString().Trim();
        public static string Name(this MethodDeclarationSyntax? syntax) => syntax?.Identifier.Text.Trim();
    }
}