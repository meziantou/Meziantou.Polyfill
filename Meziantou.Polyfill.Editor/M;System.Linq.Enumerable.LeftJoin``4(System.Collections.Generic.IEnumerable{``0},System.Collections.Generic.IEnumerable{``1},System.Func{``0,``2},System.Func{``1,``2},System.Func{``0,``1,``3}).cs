using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner?, TResult> resultSelector)
    {
        return LeftJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer: null);
    }
}
