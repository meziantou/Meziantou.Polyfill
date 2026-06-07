using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static void Sort<TKey, TValue>(this Span<TKey> keys, Span<TValue> items, Comparison<TKey> comparison)
    {
        if (keys.Length != items.Length)
            throw new ArgumentException("The keys and items spans must have the same length.");
        var keyArray = keys.ToArray();
        var itemArray = items.ToArray();
        Array.Sort(keyArray, itemArray, Comparer<TKey>.Create(comparison));
        keyArray.CopyTo(keys);
        itemArray.CopyTo(items);
    }
}
