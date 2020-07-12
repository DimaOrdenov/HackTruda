using System;
using System.Collections.Generic;

namespace HackTruda.Extensions
{
    /// <summary>
    /// Расширения для коллекций.
    /// </summary>
    public static class ICollectionExtension
    {
        public static bool TryRemove<T>(this ICollection<T> list, T item)
        {
            if (list?.Contains(item) == true)
            {
                list.Remove(item);

                return true;
            }

            return false;
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary?.ContainsKey(key) == false)
            {
                dictionary.Add(key, value);

                return true;
            }

            return false;
        }

        public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary?.ContainsKey(key) == true)
            {
                dictionary.Remove(key);

                return true;
            }

            return false;
        }
    }
}
