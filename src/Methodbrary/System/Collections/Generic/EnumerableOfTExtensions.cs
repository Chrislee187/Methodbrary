using System;
using System.Collections.Generic;
using System.Linq;

namespace Methodbrary.System.Collections.Generic
{
    public static class EnumerableOfTExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static bool None<TSource>(this IEnumerable<TSource> source)
            => !source.Any();

        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> selector)
            => !source.Any(selector);

        public static IEnumerable<string> ToStrings<TSource>(this IEnumerable<TSource> source)
            => source.Select(i => i.ToString());

        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var el in source)
            {
                yield return el;
                if (predicate(el))
                    yield break;
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            var enumerable = source as List<T> ?? source.ToList();
            foreach (var el in enumerable)
            {
                action(el);
            }

            return enumerable;
        }

        public static TSource Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal> selector)
        {
            var list = source.ToList();
            if (!list.Any()) return default(TSource);
            var min = Enumerable.Min(list, selector);

            return list.FirstOrDefault(s => selector(s) == min);
        }
        public static TSource Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal> selector)
        {
            var list = source.ToList();
            if (!list.Any()) return default(TSource);
            var max = Enumerable.Max(list, selector);

            return list.FirstOrDefault(s => selector(s) == max);
        }

        public static string ToString<TSource>(this IEnumerable<TSource> list, string delimiter = ", ") => 
            string.Join(delimiter, list.Select(i => i.ToString()));

    }
}