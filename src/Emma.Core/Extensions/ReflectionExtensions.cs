using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Emma.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool IsStatic(this Type type) => type.IsAbstract && type.IsSealed;
        
        public static IEnumerable<MethodInfo> ExtensionMethods(this Type type)
        {
            if (!type.IsStatic()) return new MethodInfo[] { };

            return type
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(mi => CustomAttributeExtensions.IsDefined((MemberInfo) mi, typeof(ExtensionAttribute)))
                ;
        }

        public static IEnumerable<MethodInfo> ExtensionMethods(this Assembly assembly)
        {
            var infos = new List<MethodInfo>();

            foreach (var staticTypes in assembly.GetExportedTypes()
                .Where(t => t.IsStatic()))
            {
                infos.AddRange(staticTypes.ExtensionMethods());
            }

            return infos;
        }
    }

}
