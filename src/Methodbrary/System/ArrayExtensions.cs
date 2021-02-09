using System.ComponentModel.DataAnnotations;

namespace Methodbrary.System
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Return default index value if index out of bounds
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <param name="defaultIndex"></param>
        /// <returns></returns>
        public static T WrapIndex<T>(this T[] value, int index)
            => index > value.Length
                ? value[0]
                : value[index];
        public static T CapIndex<T>(this T[] value, int index)
            => index > value.Length
                ? value[^1]
                : value[index];
    }
}