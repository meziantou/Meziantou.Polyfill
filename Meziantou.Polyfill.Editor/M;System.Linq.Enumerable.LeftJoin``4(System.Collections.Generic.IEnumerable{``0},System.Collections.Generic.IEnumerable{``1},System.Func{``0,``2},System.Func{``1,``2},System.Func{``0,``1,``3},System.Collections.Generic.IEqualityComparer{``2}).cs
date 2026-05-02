using System;
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner?, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
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

        return LeftJoinIterator(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
    }

    private static IEnumerable<TResult> LeftJoinIterator<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner?, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
    {
        using var outerEnumerator = outer.GetEnumerator();
        if (!outerEnumerator.MoveNext())
        {
            yield break;
        }

        var innerLookup = inner.ToLookup(innerKeySelector, comparer);
        do
        {
            var outerItem = outerEnumerator.Current;
            var innerGroup = innerLookup[outerKeySelector(outerItem)];
            using var innerGroupEnumerator = innerGroup.GetEnumerator();
            if (innerGroupEnumerator.MoveNext())
            {
                do
                {
                    yield return resultSelector(outerItem, innerGroupEnumerator.Current);
                }
                while (innerGroupEnumerator.MoveNext());
            }
            else
            {
                yield return resultSelector(outerItem, default);
            }
        }
        while (outerEnumerator.MoveNext());
    }
}
