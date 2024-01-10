using System;

static partial class PolyfillExtensions
{
    public static int IndexOfAnyExcept<T>(this Span<T> span, T value) where T : IEquatable<T>?
    {
        return IndexOfAnyExcept((ReadOnlySpan<T>)span, value);
    }
}
