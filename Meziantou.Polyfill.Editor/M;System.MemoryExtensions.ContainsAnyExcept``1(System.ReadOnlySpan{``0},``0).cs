using System;

static partial class PolyfillExtensions
{
    public static bool ContainsAnyExcept<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
    {
        return span.IndexOfAnyExcept(value) >= 0;
    }
}