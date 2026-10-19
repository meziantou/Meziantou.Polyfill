using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static bool Contains<T>(this ReadOnlySpan<T> span, T value, IEqualityComparer<T>? comparer = default)
    {
        comparer ??= EqualityComparer<T>.Default;
        foreach (var item in span)
        {
            if (comparer.Equals(item, value))
                return true;
        }

        return false;
    }
}
