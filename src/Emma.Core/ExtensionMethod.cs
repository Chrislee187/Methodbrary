using System;
using System.Linq;
using System.Text;

namespace Emma.Core
{
    public class ExtensionMethod
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Global -- Serialization
        public string Name { get; set;  }
        public string ExtendingType { get; set; }
        public string ReturnType { get; set; }
        public string[] ParamTypes { get; set; }
        public ExtensionMethodSourceType SourceType { get; set; }
        public object Source { get; set; }
        public string SourceLocation { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Global
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        // ReSharper disable once UnusedMember.Global -- JSON Serialisation
        public ExtensionMethod()
        {
            
        }

        public ExtensionMethod(string name, string extendingType, string returnType, string[] paramTypes,
            ExtensionMethodSourceType sourceType, object source,
            DateTimeOffset lastUpdated, string sourceLocation = null)
        {
            Name = name;
            ExtendingType = fixTypes(extendingType);
            ReturnType = fixTypes(returnType);
            ParamTypes = paramTypes.Select(fixTypes).ToArray();
            SourceType = sourceType;
            Source = source;
            SourceLocation = sourceLocation;
            LastUpdated = lastUpdated;
        }

        private string fixTypes(string type)
        {
            // NOTE: Some hacky code to make types from reflective mechanisms match the strings in source code files
            // only really used for the test comparisons

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
            // sb.Append($"{Path.GetFileName(SourceLocation)}:");
            sb.Append($"<{ExtendingType}>.{Name}(");
            sb.Append(string.Join(',', ParamTypes));
            sb.Append($") = {ReturnType}");

            return sb.ToString();
        }

        protected bool Equals(ExtensionMethod other) => ToString() == other.ToString();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExtensionMethod) obj);
        }

        public override int GetHashCode() => ToString().GetHashCode();
    }
}