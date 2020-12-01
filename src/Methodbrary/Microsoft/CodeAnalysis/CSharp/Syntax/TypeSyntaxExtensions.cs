using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Emma.Core.Extensions
{
    public static class TypeSyntaxExtensions
    {
        public static string Name(this TypeSyntax syntax) => syntax.GetText().ToString().Trim();

    }
}