using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emma.Core
{
    public class ExtensionMethod
    {
        private string _returnType;
        private string _extendingType;
        private string[] _paramTypes;

        // ReSharper disable UnusedAutoPropertyAccessor.Global
        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Global -- Serialization
        public string Name { get; set;  }

        public string ExtendingType
        {
            get => _extendingType;
            protected set => _extendingType = fixTypes(value);
        }


        public string ReturnType
        {
            get => _returnType;
            protected set => _returnType = fixTypes(value);
        }

        public string[] ParamTypes
        {
            get => _paramTypes;
            protected set => _paramTypes = value.Select(fixTypes).ToArray();
        }

        public ExtensionMethodSourceType SourceType { get; set; }
        public object Source { get; set; }
        public string SourceLocation { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public string ClassName { get; set; }
        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Global
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        // ReSharper disable once UnusedMember.Global -- JSON Serialisation
        public ExtensionMethod()
        {
            
        }

        public ExtensionMethod(string name, string extendingType, string returnType, IEnumerable<string> paramTypes,
            ExtensionMethodSourceType sourceType, object source,
            DateTimeOffset lastUpdated, string sourceLocation, string className)
        {
            Name = name;
            ExtendingType = fixTypes(extendingType);
            ReturnType = fixTypes(returnType);
            ParamTypes = paramTypes.Select(fixTypes).ToArray();
            SourceType = sourceType;
            Source = source;
            SourceLocation = sourceLocation;
            LastUpdated = lastUpdated;
            ClassName = className;
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
            sb.Append($"{ClassName}");
            sb.Append($".{Name}(this {ExtendingType}");
            if (ParamTypes != null && ParamTypes.Any())
            {
                sb.Append($", ");
                sb.Append(string.Join(", ", ParamTypes));
            }
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