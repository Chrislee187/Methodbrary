using System;

namespace Emma.Core
{
    public class QueryMatchers
    {
        public static bool String(string text, string compare, bool matchCase, StringMatchMode matchMode = StringMatchMode.Equals)
        {
            var left = text;
            var right = compare;

            if (matchCase)
            {
                left = left.ToLowerInvariant();
                right = right.ToLowerInvariant();
            }

            Func<string, string, bool> matcher = matchMode switch
            {
                StringMatchMode.Equals => (n, m) => m.Equals(n),
                StringMatchMode.StartsWith => (n, m) => m.StartsWith(n),
                StringMatchMode.EndsWith => (n, m) => m.EndsWith(n),
                StringMatchMode.Contains => (n, m) => m.Contains(n),
                _ => throw new ArgumentOutOfRangeException()
            };

            return matcher(text, compare);
        }
    }
}