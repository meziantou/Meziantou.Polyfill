using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<TResult> RightJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter?, TInner, TResult> resultSelector)
    {
        return RightJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer: null);
    }
}
