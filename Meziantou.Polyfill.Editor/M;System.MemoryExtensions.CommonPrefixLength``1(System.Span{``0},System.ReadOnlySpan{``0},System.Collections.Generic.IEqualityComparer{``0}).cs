using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static int CommonPrefixLength<T>(this Span<T> span, ReadOnlySpan<T> other, IEqualityComparer<T> comparer)
    {
        return CommonPrefixLength((ReadOnlySpan<T>)span, other, comparer);
    }
}
