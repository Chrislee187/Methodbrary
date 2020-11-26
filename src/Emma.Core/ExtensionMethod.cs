using System;
using System.Linq;
using System.Text;

namespace Emma.Core
{
    public class ExtensionMethod
    {
        public readonly string Container;
        public readonly string Name;
        public readonly string ExtendingType;
        public readonly string ReturnType;
        public readonly string[] ParamTypes;

        public ExtensionMethod(string name, string extendingType, string returnType, string[] paramTypes)
        {
            Name = name;
            ExtendingType = extendingType;
            ReturnType = returnType;
            ParamTypes = paramTypes;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{ExtendingType}.{Name}(");
            sb.Append(string.Join(',', ParamTypes));
            sb.Append($") = {ReturnType}");

            return sb.ToString();
        }
    }
}