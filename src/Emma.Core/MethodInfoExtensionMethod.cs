using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Emma.Core
{
    internal class MethodInfoExtensionMethod : ExtensionMethod
    {
        public MethodInfoExtensionMethod(MethodInfo mi, DateTime lastUpdated)
        {
            if (!mi.IsStatic) throw new MethodAccessException($"Method '{mi.Name}' is not an extension method");
            var prms = mi.GetParameters();

            if (prms.Length < 1) throw new MethodAccessException($"Method '{mi.Name}' is not an extension method");

            var extendingTypeName = prms[0].ParameterType.Name;
            
            var paramTypeNames = prms[1..].Select(GetParamType);

            var returnTypeName = mi.ReturnType.Name;
            if (mi.ReturnType.IsGenericType)
            {
                returnTypeName = NormaliseReturnType(mi);
            }

            Name = mi.Name;
            ExtendingType = NormaliseDotNetType(extendingTypeName);
            ReturnType = NormaliseDotNetType(returnTypeName);
            ParamTypes = paramTypeNames.Select(NormaliseDotNetType).ToArray();
            SourceType = ExtensionMethodSourceType.Assembly;
            Source = null;
            LastUpdated = lastUpdated;
            SourceLocation = $"{mi.DeclaringType?.Assembly.Location}:{mi.DeclaringType?.FullName}";
            ClassName = mi.DeclaringType?.Name;
        }

        private string GetParamType(ParameterInfo pi)
        {
            if (pi.ParameterType.IsGenericType)
            {
                var sb = new StringBuilder(50);
                sb.Append(pi.ParameterType.Name[..(pi.ParameterType.Name.IndexOf('`'))]);
                var types = pi.ParameterType.GenericTypeArguments;
                sb.Append("<");
                sb.Append(string.Join(", ", types.Select(t => NormaliseDotNetType(t.Name))));
                sb.Append(">");
                return sb.ToString();
            }
            else
            {
                return NormaliseDotNetType(pi.ParameterType.Name);
            }
        }

        protected string NormaliseReturnType(MethodInfo mi)
        {
            var returnTypeName = mi.ReturnType.Name;

            var genericTypes = mi.ReturnType.GenericTypeArguments.Select(a => NormaliseDotNetType(a.Name));
            var genericArgs = string.Join(",", genericTypes);
            returnTypeName = returnTypeName.Substring(0, returnTypeName.IndexOf("`", StringComparison.Ordinal));
            returnTypeName += $"<{genericArgs}>";
            return returnTypeName;
        }
    }
}