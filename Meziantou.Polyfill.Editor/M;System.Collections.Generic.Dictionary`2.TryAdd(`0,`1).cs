using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(dictionary);

        if (dictionary.ContainsKey(key))
        {
            return false;
        }

        dictionary.Add(key, value);

        return true;
    }
}