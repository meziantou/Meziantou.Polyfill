using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static void Sort<TKey, TValue, TComparer>(this Span<TKey> keys, Span<TValue> items, TComparer comparer) where TComparer : IComparer<TKey>?
    {
        if (keys.Length != items.Length)
            throw new ArgumentException("The keys and items spans must have the same length.");
        var keyArray = keys.ToArray();
        var itemArray = items.ToArray();
        Array.Sort(keyArray, itemArray, comparer);
        keyArray.CopyTo(keys);
        itemArray.CopyTo(items);
    }
}
