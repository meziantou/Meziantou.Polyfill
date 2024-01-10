using System;

static partial class PolyfillExtensions
{
    public static bool ContainsAnyExcept<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
    {
        return span.IndexOfAnyExcept(values) >= 0;
    }
}