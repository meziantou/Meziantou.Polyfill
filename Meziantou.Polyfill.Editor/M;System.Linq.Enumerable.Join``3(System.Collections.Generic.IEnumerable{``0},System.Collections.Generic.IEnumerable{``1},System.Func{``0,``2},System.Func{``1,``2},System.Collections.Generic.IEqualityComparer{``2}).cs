using System;
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IEnumerable<(TOuter Outer, TInner Inner)> Join<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, IEqualityComparer<TKey>? comparer = null)
    {
        return outer.Join(inner, outerKeySelector, innerKeySelector, static (outer, inner) => (outer, inner), comparer);
    }
}
