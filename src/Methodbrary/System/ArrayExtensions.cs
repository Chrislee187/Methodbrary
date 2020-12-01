namespace Methodbrary.System
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Return default value if index out of bounds
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T DefaultingIndex<T>(this T[] value, int index, T defaultValue = default(T))
            => index + 1 > value.Length
                ? defaultValue
                : value[index];
    }
}