using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector)
    {
        return IntersectBy(first, second, keySelector, null);
    }
}