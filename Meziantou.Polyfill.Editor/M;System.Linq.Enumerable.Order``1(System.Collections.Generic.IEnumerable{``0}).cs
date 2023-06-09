﻿using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IOrderedEnumerable<T> Order<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(_ => _);
    }
}