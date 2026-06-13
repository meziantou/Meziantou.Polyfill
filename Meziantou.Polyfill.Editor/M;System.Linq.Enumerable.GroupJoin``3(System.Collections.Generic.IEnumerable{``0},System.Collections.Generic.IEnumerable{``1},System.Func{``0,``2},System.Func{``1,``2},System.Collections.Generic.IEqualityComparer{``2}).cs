using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IEnumerable<IGrouping<TOuter, TInner>> GroupJoin<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, IEqualityComparer<TKey>? comparer = null)
    {
        return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, static (outer, elements) => (IGrouping<TOuter, TInner>)new GroupJoinGroupingPolyfill<TOuter, TInner>(outer, elements), comparer);
    }
}

file sealed class GroupJoinGroupingPolyfill<TKey, TElement>(TKey key, IEnumerable<TElement> elements) : IGrouping<TKey, TElement>
{
    public TKey Key => key;

    public IEnumerator<TElement> GetEnumerator() => elements.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
