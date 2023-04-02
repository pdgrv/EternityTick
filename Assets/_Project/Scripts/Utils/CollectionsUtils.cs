using System;
using System.Collections.Generic;
using System.Linq;

namespace Eternity.Utils
{
    public static class CollectionsUtils
    {
        public static T GetRandom<T>(this IList<T> list, Random rng)
        {
            int index = rng.Next(0, list.Count);
            return list[index];
        }
        
        public static Dictionary<TValue, TKey> SwapKeysAndValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary.ToDictionary(t => t.Value, t => t.Key);
        }
    }
}