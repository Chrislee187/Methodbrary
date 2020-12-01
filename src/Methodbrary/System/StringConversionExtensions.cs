﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Methodbrary.System
{
    /// <summary>
    /// Safe string conversions
    /// </summary>
    public static class StringConversionExtensions
    {
        private static readonly char[] EndOfLineChars = {'\r', '\n'};

        public static int ToInt(this string value, int @default = 0)
            => int.TryParse(value, out var result) ? result : @default;

        public static float ToFloat(this string value, float @default = 0f)
            => float.TryParse(value, out var result) ? result : @default;

        public static double ToDouble(this string value, double @default = 0d)
            => double.TryParse(value, out var result) ? result : @default;

        public static decimal ToDecimal(this string value, decimal @default = 0m)
            => decimal.TryParse(value, out var result) ? result : @default;

        public static bool ToBool(this string value, bool @default = false)
        {
            if (value.Length == 0) return false;

            var c1 = value.ToLower()[0];
            return new[] {'y', 't', '1'}.Any(c => c == c1);
        }

        public static DateTime? ToDateTime(this string value, DateTime? dflt = null)
            => DateTime.TryParse(value, out var result)
                ? result
                : dflt ?? DateTime.MinValue;

        public static Guid ToGuid(this string value, Guid? dflt = null)
            => Guid.TryParse(value, out var result)
                ? result
                : dflt ?? Guid.Empty;

        public static Stream ToStream(this string value)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(value);
                writer.Flush();
                stream.Position = 0;
            }

            return stream;
        }

        public static StreamReader ToStreamReader(this string source) => new StreamReader(source.ToStream());

        public static byte[] ToBytes(this string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string[] ToLines(this string data)
        {
            var lines = new List<string>();

            using (var sr = new StringReader(data))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }

        /// <summary>
        /// Searchs for the default static TryParse(string, out T) that exists on many .NET types to
        /// dynamically convert a string to any type.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ToTypeInstance(this string value, Type type)
        {
            if (type == typeof(string)) return value;

            var tryParseParamsFilter = new[] {typeof(string), type.MakeByRefType()};
            var tryParseMethod = type.GetMethod("TryParse", tryParseParamsFilter);

            if (tryParseMethod == null)
            {
                throw new SystemException($"No static TryParse(string, out {type.Name}) found on {type.Name}");
            }

            var parameters = new object[] {value, null};
            var result = (bool) tryParseMethod.Invoke(null, parameters);

            if (result)
            {
                return parameters[1];
            }

            throw new SystemException($"Attempt to parse '{value}' using {type}.TryParse() failed");
        }


        public static string ToValidFilename(this string source, string invalidChars)
            => source.ToValidFilename(invalidChars.ToCharArray());

        public static string ToValidFilename(this string source, params char[] invalidChars)
        {
            if (!invalidChars.Any())
            {
                invalidChars = Path.GetInvalidFileNameChars();
            }

            return source.ReplaceChars(invalidChars);
        }
    }
}