using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static void TrimExcess<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, int capacity) where TKey : notnull
    {
        if (capacity < dictionary.Count)
            throw new ArgumentOutOfRangeException(nameof(capacity));
    }
}
