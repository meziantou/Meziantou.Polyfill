using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static int CommonPrefixLength<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other, IEqualityComparer<T> comparer)
    {
        if (typeof(T).IsValueType && (comparer is null || ReferenceEquals(comparer, EqualityComparer<T>.Default)))
        {
            return CommonPrefixLength(span, other);
        }

        var minLength = Math.Min(span.Length, other.Length);
        comparer ??= EqualityComparer<T>.Default;
        for (var i = 0; i < minLength; i++)
        {
            if (!comparer.Equals(span[i], other[i]))
            {
                return i;
            }
        }

        return minLength;
    }
}
