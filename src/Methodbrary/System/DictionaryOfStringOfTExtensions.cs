using System;
using System.Collections.Generic;
using System.Linq;

namespace Methodbrary.System
{
    public static class DictionaryExtensions
    {
        public static bool ContainsKeyInsensitive<TValue>(this IDictionary<string, TValue> dict, string key)
            => dict.Any(d => d.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
        public static TValue GetValueInsensitive<TValue>(this IDictionary<string, TValue> dict, string key)
            => dict.Single(d => d.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)).Value;
    }
}