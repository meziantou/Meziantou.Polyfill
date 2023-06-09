﻿using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IOrderedEnumerable<T> OrderDescending<T>(this IEnumerable<T> source, IComparer<T>? comparer)
    {
        return source.OrderByDescending(_ => _, comparer);
    }
}