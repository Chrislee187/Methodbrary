using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Methodbrary.System
{
    public static class MethodbraryStringExtensions
    {
        public static string ReplaceWithSingleSpace(this string s, string pattern)
            => Regex.Replace(s, $"({pattern})++", " ");

        public static string Repeat(this string s, int times)
            => Enumerable.Range(1, times).Aggregate("", (a, i) => a + s);
        public static string Repeat(this char c, int times)
            => new string(c, times);

        public static StringBuilder RepeatStringBuilder(this string s, int times)
        {
            var sb = new StringBuilder(s.Length * times);

            for (var i = 0; i < times; i++)
            {
                sb.Append(s);
            }

            return sb;
        }

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
        public static string Unwrap(this string source, char wrapper)
        {
            if (string.IsNullOrEmpty(source)) return source;

            if (source.First() == wrapper && source.Last() == wrapper)
            {
                return source.AsSpan(1, source.Length - 2).ToString();
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
