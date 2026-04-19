using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static bool StartsWith<T>(this ReadOnlySpan<T> span, T value, IEqualityComparer<T>? comparer = default)
    {
        if (span.Length == 0)
            return false;

        comparer ??= EqualityComparer<T>.Default;
        return comparer.Equals(span[0], value);
    }
}
