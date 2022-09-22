using System;
using System.Collections.Generic;

namespace Wemogy.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Put<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
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
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return default(TValue);
        }

        public static Guid GetGuid<TKey>(this IDictionary<TKey, string> dictionary, TKey key)
        {
            if (dictionary.ContainsKey(key))
            {
                return Guid.Parse(dictionary[key]);
            }

            return default(Guid);
        }

        public static Guid? GetNullableGuid<TKey>(this IDictionary<TKey, string> dictionary, TKey key)
        {
            if (dictionary.ContainsKey(key))
            {
                return Guid.Parse(dictionary[key]);
            }

            return null;
        }

        public static void AddItem<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue item)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new List<TValue>());
            }

            dictionary[key].Add(item);
        }
    }
}
