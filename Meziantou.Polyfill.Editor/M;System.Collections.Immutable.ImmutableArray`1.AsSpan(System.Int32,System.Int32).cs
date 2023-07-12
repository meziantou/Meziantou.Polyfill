using System;
using System.Collections.Immutable;

static partial class PolyfillExtensions
{
    public static ReadOnlySpan<T> AsSpan<T>(this ImmutableArray<T> target, int start, int length)
    {
        return target.AsSpan().Slice(start, length);
    }
}