using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static void TrimExcess<T>(this HashSet<T> set, int capacity)
    {
        if (capacity < set.Count)
            throw new ArgumentOutOfRangeException(nameof(capacity));
    }
}
