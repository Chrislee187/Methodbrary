using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Methodbrary.System
{
    public static class StringExtensions
    {
        public static string Repeat(this string s, int times)
            => Enumerable.Range(1, times).Aggregate("", (a, i) => a + s);

        private static readonly char[] QuoteChars = "'\"".ToCharArray();

        public static string Dequote(this string source)
        {
            if (QuoteChars.Contains(source.First())
                && QuoteChars.Contains(source.Last()))
            {
                return source.Substring(1, source.Length - 2);
            }

            return source;
        }

        public static string Wrap(this string source, string prefix = "'", string postfix = "'")
        {
            if (QuoteChars.Contains(source.First())
                && QuoteChars.Contains(source.Last()))
            {
                return source.Substring(1, source.Length - 2);
            }

            return source;

        }

        public static string Reverse(this string text)
        {
            var enumerator = StringInfo.GetTextElementEnumerator(text);

            var elements = new List<string>();
            while (enumerator.MoveNext())
                elements.Add(enumerator.GetTextElement());

            elements.Reverse();
            return string.Concat(elements);
        }

        public static string RemoveChars(this string source, char[] chars) =>
            new string(source.Where(m => !chars.Contains(m)).ToArray());

        public static string ReplaceChars(this string source, char[] chars, char newChar = '_') =>
            chars.Aggregate(source, (s, c) => s.Replace(c, newChar));


    }
}
