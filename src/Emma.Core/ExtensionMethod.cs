using System;
using System.Linq;
using System.Text;

namespace Emma.Core
{
    public class ExtensionMethod
    {
        public readonly string Name;
        public readonly string ExtendingType;
        public readonly string ReturnType;
        public readonly string[] ParamTypes;

        public ExtensionMethod(string name, string extendingType, string returnType, string[] paramTypes)
        {
            Name = name;
            ExtendingType = fixTypes(extendingType);
            ReturnType = fixTypes(returnType);
            ParamTypes = paramTypes.Select(fixTypes).ToArray();

        }

        private string fixTypes(string type)
        {
            if (type.ToLower() == "boolean") type = "bool";

            if (type == "String") type = "string";

            if (type.EndsWith("?")) type = type[..^1];

            return type;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{ExtendingType}.{Name}(");
            sb.Append(string.Join(',', ParamTypes));
            sb.Append($") = {ReturnType}");

            return sb.ToString();
        }

        protected bool Equals(ExtensionMethod other)
        {
            return ToString() == other.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExtensionMethod) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ExtendingType, ReturnType, ParamTypes);
        }
    }
}