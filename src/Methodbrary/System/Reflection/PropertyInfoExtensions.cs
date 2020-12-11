using System;
using System.Linq;
using System.Reflection;

namespace Methodbrary.System.Reflection
{
    public static class PropertyInfoExtensions
    {
        public static bool HasSerializationIgnoreAttribute(this PropertyInfo info)
            => info.CustomAttributes
                .Any(a => new[] { "JsonIgnore", "XmlIgnore" }
                    .Any(ignoreAttr => a.AttributeType.Name.Contains(ignoreAttr, StringComparison.InvariantCulture)));
    }
}