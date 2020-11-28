using System.Linq;

namespace Emma.Core.Extensions
{
    public static class StringExtensions
    {

        public static string ToValidFilename(this string source, string invalidChars) =>
            source.ToValidFilename(invalidChars.ToCharArray());
        public static string ToValidFilename(this string source, params char[] invalidChars)
        {
            if (!invalidChars.Any())
            {
                invalidChars = System.IO.Path.GetInvalidFileNameChars();
            }

            return source.ReplaceChars(invalidChars);
        }

        public static string RemoveChars(this string source, char[] chars) => 
            new string(source.Where(m => !chars.Contains(m)).ToArray());

        public static string ReplaceChars(this string source, char[] chars, char newChar = '_') => 
            chars.Aggregate(source, (s, c) => s.Replace(c, newChar));

    }
}