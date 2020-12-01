using System.Collections.Generic;

namespace Methodbrary.System.Collections.Generic
{
    public static class CollectionOfTExtensions
    {
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                destination.Add(item);
            }
        }
    }
}