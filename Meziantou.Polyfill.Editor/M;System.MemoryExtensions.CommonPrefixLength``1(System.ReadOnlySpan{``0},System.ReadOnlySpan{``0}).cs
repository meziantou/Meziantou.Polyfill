using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static int CommonPrefixLength<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other)
    {
        var minLength = Math.Min(span.Length, other.Length);
        for (var i = 0; i < minLength; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(span[i], other[i]))
            {
                return i;
            }
        }

        return minLength;
    }
}
