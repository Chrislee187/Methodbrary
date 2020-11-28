using System;
using System.Linq;
using System.Text;

namespace Emma.Core
{
    public class ExtensionMethod
    {
        public ExtensionMethodSourceType SourceType { get; }
        public readonly string Name;
        public readonly string ExtendingType;
        public readonly string ReturnType;
        public readonly object Source;
        public readonly string[] ParamTypes;

        public ExtensionMethod(string name, string extendingType, string returnType, string[] paramTypes, 
            ExtensionMethodSourceType sourceType, object source)
        {
            Source = source.ToString();
            SourceType = sourceType;
            Name = name;
            ExtendingType = fixTypes(extendingType);
            ReturnType = fixTypes(returnType);
            ParamTypes = paramTypes.Select(fixTypes).ToArray();

        }

        private string fixTypes(string type)
        {
            // NOTE: Some hacky code to make types from reflective mechanisms match the strings in source code files
            // only really used for test comparisons

            if (new[] {"String", "Void", "Single", "Double", "Decimal", "Object", "Char", "Boolean"}.Contains(type))
            {
                type = type.ToLowerInvariant();
            }
            
            if (type == "single") type = "float";
            if (type == "boolean") type = "bool";

            if (type.EndsWith("?")) type = type[..^1]; // NOTE: Ignore nullables for now

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