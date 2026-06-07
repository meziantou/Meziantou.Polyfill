using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static void TrimExcess<T>(this Queue<T> queue, int capacity)
    {
        if (capacity < queue.Count)
            throw new ArgumentOutOfRangeException(nameof(capacity));
    }
}
