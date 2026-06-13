using System;
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IEnumerable<TResult> FullJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter?, TInner?, TResult> resultSelector, IEqualityComparer<TKey>? comparer = null)
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

        if (resultSelector is null)
        {
            throw new ArgumentNullException(nameof(resultSelector));
        }

        return FullJoinIterator(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
    }

    private static IEnumerable<TResult> FullJoinIterator<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter?, TInner?, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
    {
        var innerLookup = inner.ToLookup(innerKeySelector, comparer);
        var matchedKeys = new HashSet<TKey>(comparer);

        foreach (var outerItem in outer)
        {
            var outerKey = outerKeySelector(outerItem);
            if (outerKey is not null)
            {
                var innerGroup = innerLookup[outerKey];
                using var enumerator = innerGroup.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    matchedKeys.Add(outerKey);
                    do
                    {
                        yield return resultSelector(outerItem, enumerator.Current);
                    }
                    while (enumerator.MoveNext());

                    continue;
                }
            }

            yield return resultSelector(outerItem, default);
        }

        foreach (var innerGroup in innerLookup)
        {
            if (!matchedKeys.Contains(innerGroup.Key))
            {
                foreach (var innerItem in innerGroup)
                {
                    yield return resultSelector(default, innerItem);
                }
            }
        }
    }
}
