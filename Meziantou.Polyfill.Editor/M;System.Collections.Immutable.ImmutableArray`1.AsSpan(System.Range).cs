using System;
using System.Collections.Immutable;

static partial class PolyfillExtensions
{
    public static ReadOnlySpan<T> AsSpan<T>(this ImmutableArray<T> target, Range range)
    {
        (int start, int length) = range.GetOffsetAndLength(target.Length);
        return target.AsSpan().Slice(start, length);
    }
}