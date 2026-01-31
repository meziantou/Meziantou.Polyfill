using System;
using System.Buffers;

static partial class PolyfillExtensions
{
    public static int IndexOfAny<T>(this Span<T> span, SearchValues<T> values) where T : IEquatable<T>?
    {
        return IndexOfAny((ReadOnlySpan<T>)span, values);
    }
}
