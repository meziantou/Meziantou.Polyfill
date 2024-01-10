using System;

static partial class PolyfillExtensions
{
    public static bool ContainsAnyExcept<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
    {
        return ContainsAnyExcept((ReadOnlySpan<T>)span, values);
    }
}