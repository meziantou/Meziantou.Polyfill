using System;

static partial class PolyfillExtensions
{
    public static bool ContainsAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
    {
        return span.IndexOfAnyExcept(value0, value1) >= 0;
    }
}
