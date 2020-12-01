using System;

namespace Methodbrary.System
{
    public static class TypeExtensions
    {
        public static bool IsStatic(this Type type) => type.IsAbstract && type.IsSealed;
    }
}