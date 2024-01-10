using System;

static partial class PolyfillExtensions
{
    public static bool ContainsAnyExcept<T>(this Span<T> span, T value0, T value1) where T : IEquatable<T>?
    {
        return ContainsAnyExcept((ReadOnlySpan<T>)span, value0, value1);
    }
}