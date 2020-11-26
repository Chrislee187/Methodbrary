using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Emma.Core
{
    public static class TypeExtensions
    {
        public static bool IsStatic(this Type type) => type.IsAbstract && type.IsSealed;
        public static MethodInfo[] ExtensionMethods(this Type type)
        {
            if (!type.IsStatic()) return new MethodInfo[] { };

            var methods = type.GetMethods(BindingFlags.Static
                                          | BindingFlags.Public)
                .Where(mi =>
                {
                    var isDefined = mi.IsDefined(typeof(ExtensionAttribute));
                    var prms = mi.GetParameters();
                    if (!prms.Any()) return false;

                    return isDefined;
                });


            return methods.ToArray();
        }

        public static MethodInfo[] ExtensionMethods(this Assembly assembly)
        {
            var infos = new List<MethodInfo>();

            foreach (var staticTypes in assembly.GetExportedTypes().Where(t => t.IsStatic()))
            {
                infos.AddRange(staticTypes.ExtensionMethods());
            }

            return infos.ToArray();
        }
    }

}
