using System;
using System.Collections.Immutable;

static partial class PolyfillExtensions
{
    public static ReadOnlySpan<T> AsSpan<T>(this ImmutableArray<T> target)
    {
        var result = new T[target.Length];
        for (int i = 0; i < target.Length; i++)
        {
            result[i] = target[i];
        }

        return target.AsSpan();
    }
}