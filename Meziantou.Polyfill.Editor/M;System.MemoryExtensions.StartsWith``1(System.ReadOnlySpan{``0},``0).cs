using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static bool StartsWith<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
    {
        if (span.Length == 0)
            return false;

        return EqualityComparer<T>.Default.Equals(span[0], value);
    }
}
