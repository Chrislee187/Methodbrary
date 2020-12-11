﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Methodbrary.System.Reflection
{
    public static class MethodInfoExtensions
    {
        public static IEnumerable<MethodInfo> ExtensionMethods(this Type type)
        {
            if (!type.IsStatic()) return new MethodInfo[] { };

            return type
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(mi => mi.IsDefined(typeof(ExtensionAttribute)))
                ;
        }

        public static IEnumerable<MethodInfo> ExtensionMethods(this Assembly assembly)
        {
            var infos = new List<MethodInfo>();

            foreach (var staticType in assembly.GetExportedTypes()
                .Where(t => t.IsStatic()))
            {
                infos.AddRange(staticType.ExtensionMethods());
            }

            return infos;
        }
    }

}
