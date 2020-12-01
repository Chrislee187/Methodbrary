using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Emma.Core.Extensions
{
    public static class MethodDeclarationSyntaxExtensions
    {
        public static bool IsExtensionMethod(this MethodDeclarationSyntax syntax) 
            => syntax.IsPublic() && syntax.IsStatic() 
                                 && (syntax.ParameterList.Parameters.Any() 
                                     && syntax.ParameterList.Parameters.First().IsThis());

        public static string Name(this MethodDeclarationSyntax syntax) => syntax.Identifier.Text.Trim();


    }
}