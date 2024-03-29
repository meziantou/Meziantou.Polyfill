﻿using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static TSource? MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        return source.MinBy(keySelector, comparer: null);
    }
}