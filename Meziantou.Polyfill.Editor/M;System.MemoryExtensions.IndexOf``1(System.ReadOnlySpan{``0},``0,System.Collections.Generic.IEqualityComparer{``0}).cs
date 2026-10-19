using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static int IndexOf<T>(this ReadOnlySpan<T> span, T value, IEqualityComparer<T>? comparer = default)
    {
        comparer ??= EqualityComparer<T>.Default;
        for (var i = 0; i < span.Length; i++)
        {
            if (comparer.Equals(span[i], value))
            {
                return i;
            }
        }

        return -1;
    }
}
