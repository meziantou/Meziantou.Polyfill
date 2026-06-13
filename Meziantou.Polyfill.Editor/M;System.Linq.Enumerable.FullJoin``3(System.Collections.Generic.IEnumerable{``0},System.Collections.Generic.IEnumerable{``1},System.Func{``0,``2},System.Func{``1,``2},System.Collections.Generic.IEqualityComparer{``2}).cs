using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<(TOuter? Outer, TInner? Inner)> FullJoin<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, IEqualityComparer<TKey>? comparer = null)
    {
        return FullJoin(outer, inner, outerKeySelector, innerKeySelector, static (outer, inner) => (outer, inner), comparer);
    }
}
