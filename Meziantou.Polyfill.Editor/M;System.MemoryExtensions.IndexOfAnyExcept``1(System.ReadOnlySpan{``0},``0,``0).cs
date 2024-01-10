using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static int IndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
    {
        var equalityComparer = EqualityComparer<T>.Default;
        for (var i = 0; i < span.Length; i++)
        {
            if (!equalityComparer.Equals(value0, span[i]) && !equalityComparer.Equals(value1, span[i]))
            {
                return i;
            }
        }

        return -1;
    }
}
