using System;

static partial class PolyfillExtensions
{
    public static int CommonPrefixLength<T>(this Span<T> span, ReadOnlySpan<T> other)
    {
        return CommonPrefixLength((ReadOnlySpan<T>)span, other);
    }
}
