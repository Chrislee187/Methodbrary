using System;
using System.Collections.Generic;
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
        public string ClassName { get; set; }
        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Global
        // ReSharper restore UnusedAutoPropertyAccessor.Global
        
        // ReSharper disable once UnusedMember.Global -- JSON Serialisation
        // ReSharper disable once MemberCanBeProtected.Global -- JSON Serialisation
        public ExtensionMethod()
        {
            
        }

        public ExtensionMethod(string name, string extendingType, string returnType, IEnumerable<string> paramTypes,
            ExtensionMethodSourceType sourceType, object source,
            DateTimeOffset lastUpdated, string sourceLocation, string className)
        {
            Name = name;
            ExtendingType = NormaliseDotNetType(extendingType);
            ReturnType = NormaliseDotNetType(returnType);
            ParamTypes = paramTypes.Select(NormaliseDotNetType).ToArray();
            SourceType = sourceType;
            Source = source;
            SourceLocation = sourceLocation;
            LastUpdated = lastUpdated;
            ClassName = className;
        }

        protected string NormaliseDotNetType(string type)
        {
            // NOTE: Some hacky code to make types from reflective mechanisms match the strings in source code files
            // only really used for the test comparisons

            if (new[] {"Byte", "String", "Void", "Single", "Double", "Decimal", "Object", "Char", "Boolean"}.Contains(type))
            {
                type = type.ToLowerInvariant();
            }
            
            if (type == "single") type = "float";
            if (type == "boolean") type = "bool";
            if (type == "Int32") type = "int";
            if (type == "Int64") type = "long";

            if (type.EndsWith("?")) type = type[..^1]; // NOTE: Ignore nullables for now

            return type;
        }

        #region ToString()

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

        #endregion 
        #region Equality

        protected bool Equals(ExtensionMethod other) => ToString() == other.ToString();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExtensionMethod)obj);
        }

        public override int GetHashCode() => ToString().GetHashCode(); 

        #endregion
    }
}