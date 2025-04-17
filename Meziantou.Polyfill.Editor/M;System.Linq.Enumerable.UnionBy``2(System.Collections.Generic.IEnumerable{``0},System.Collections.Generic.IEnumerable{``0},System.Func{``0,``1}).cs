using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<TSource> UnionBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
    {
        return first.UnionBy(second, keySelector, null);
    }
}
