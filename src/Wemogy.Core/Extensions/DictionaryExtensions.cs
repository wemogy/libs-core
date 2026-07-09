using System;
using System.Collections.Generic;

namespace Wemogy.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Put<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            dictionary[key] = value;
        }

        /**
         * Merges all entries from the given dictionaryB into dictionaryA
         */
        public static void Merge<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionaryA,
            IDictionary<TKey, TValue> dictionaryB)
        {
            foreach (var item in dictionaryB)
            {
                dictionaryA.Put(item.Key, item.Value);
            }
        }

        public static TValue? Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                return value;
            }

            return default(TValue);
        }

        public static Guid GetGuid<TKey>(this IDictionary<TKey, string> dictionary, TKey key)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                return Guid.Parse(value);
            }

            return Guid.Empty;
        }

        public static Guid? GetNullableGuid<TKey>(this IDictionary<TKey, string> dictionary, TKey key)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                return Guid.Parse(value);
            }

            return null;
        }

        public static void AddItem<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue item)
            where TKey : notnull
        {
            if (!dictionary.TryGetValue(key, out var list))
            {
                list = new List<TValue>();
                dictionary[key] = list;
            }

            list.Add(item);
        }
    }
}
