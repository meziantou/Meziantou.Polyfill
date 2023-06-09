﻿using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
    {
        var hashSet = new HashSet<TKey>(comparer);
        foreach (var item in source)
        {
            var key = keySelector(item);
            if (hashSet.Add(key))
                yield return item;
        }
    }
}