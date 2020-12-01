using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Emma.Core.Extensions
{
    public static class BaseParameterSyntaxExtensions
    {
        public static bool IsThis(this BaseParameterSyntax syntax)
            => syntax.Modifiers.Any(m => m.Kind() == SyntaxKind.ThisKeyword);

    }
}