using System;
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IEnumerable<(TOuter? Outer, TInner Inner)> RightJoin<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, IEqualityComparer<TKey>? comparer = null)
    {
        if (outer is null)
        {
            throw new ArgumentNullException(nameof(outer));
        }

        if (inner is null)
        {
            throw new ArgumentNullException(nameof(inner));
        }

        if (outerKeySelector is null)
        {
            throw new ArgumentNullException(nameof(outerKeySelector));
        }

        if (innerKeySelector is null)
        {
            throw new ArgumentNullException(nameof(innerKeySelector));
        }

        return RightJoinImplementation.RightJoinIterator(outer, inner, outerKeySelector, innerKeySelector, comparer);
    }
}

file static class RightJoinImplementation
{
    public static IEnumerable<(TOuter? Outer, TInner Inner)> RightJoinIterator<TOuter, TInner, TKey>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, IEqualityComparer<TKey>? comparer)
    {
        var outerLookup = outer.ToLookup(outerKeySelector, comparer);
        foreach (var innerItem in inner)
        {
            var innerKey = innerKeySelector(innerItem);
            if (innerKey is not null)
            {
                var outerGroup = outerLookup[innerKey];
                using var enumerator = outerGroup.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    do
                    {
                        yield return (enumerator.Current, innerItem);
                    }
                    while (enumerator.MoveNext());

                    continue;
                }
            }

            yield return (default, innerItem);
        }
    }
}
