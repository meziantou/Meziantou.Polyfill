using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<TSource> UnionBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
    {
        var set = new HashSet<TKey>(comparer);

        foreach (var element in first)
        {
            if (set.Add(keySelector(element)))
            {
                yield return element;
            }
        }

        foreach (var element in second)
        {
            if (set.Add(keySelector(element)))
            {
                yield return element;
            }
        }
    }
}