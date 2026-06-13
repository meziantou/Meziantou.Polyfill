using System;
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IEnumerable<(TOuter Outer, TInner? Inner)> LeftJoin<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, IEqualityComparer<TKey>? comparer = null)
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

        return LeftJoinTupleIterator(outer, inner, outerKeySelector, innerKeySelector, comparer);
    }

    private static IEnumerable<(TOuter Outer, TInner? Inner)> LeftJoinTupleIterator<TOuter, TInner, TKey>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, IEqualityComparer<TKey>? comparer)
    {
        var innerLookup = inner.ToLookup(innerKeySelector, comparer);
        foreach (var outerItem in outer)
        {
            var outerKey = outerKeySelector(outerItem);
            if (outerKey is not null)
            {
                var innerGroup = innerLookup[outerKey];
                using var enumerator = innerGroup.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    do
                    {
                        yield return (outerItem, enumerator.Current);
                    }
                    while (enumerator.MoveNext());

                    continue;
                }
            }

            yield return (outerItem, default);
        }
    }
}
