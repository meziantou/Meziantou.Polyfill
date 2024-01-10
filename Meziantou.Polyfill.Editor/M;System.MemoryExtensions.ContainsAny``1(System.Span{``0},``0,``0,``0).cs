using System;

static partial class PolyfillExtensions
{
    public static bool ContainsAny<T>(this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
    {
        return span.IndexOfAny(value0, value1, value2) >= 0;
    }
}
