using System;
using System.Linq;
using Emma.Core.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Emma.Core
{
    internal class MemberSyntaxExtensionMethod : ExtensionMethod
    {
        public MemberSyntaxExtensionMethod(MethodDeclarationSyntax member, DateTimeOffset lastUpdated, string className, string sourceLocation)
        {
            if (!member.IsExtensionMethod())
            {
                throw new ArgumentException($"member '{member.Name()}' is not an extension method.");
            }


            var extendingType = member.ParameterList.Parameters.First()
                .Type.Name();
            var returnType = member.ReturnType.Name();

            var prms = member.ParameterList.Parameters
                .Skip(1)
                .Select(p => p.Type?.Name())
                .ToArray();

            Name = member.Name();
            ExtendingType = NormaliseDotNetType(extendingType);
            ReturnType = NormaliseDotNetType(returnType);
            ParamTypes = prms.Select(NormaliseDotNetType).ToArray();
            SourceType = ExtensionMethodSourceType.Assembly;
            Source = null;
            LastUpdated = lastUpdated;
            SourceLocation = sourceLocation;
            ClassName = className;

        }
    }
}