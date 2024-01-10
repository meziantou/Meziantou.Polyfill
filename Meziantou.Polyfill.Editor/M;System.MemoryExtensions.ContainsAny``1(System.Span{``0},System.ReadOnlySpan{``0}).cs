using System;

static partial class PolyfillExtensions
{
    public static bool ContainsAny<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
    {
        return span.IndexOfAny(values) >= 0;
    }
}
