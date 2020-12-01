using System;
using System.Collections.Generic;
using System.Linq;

namespace Methodbrary.System
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetAll<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}