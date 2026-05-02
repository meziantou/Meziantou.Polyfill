using System;
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IEnumerable<TResult> RightJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter?, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
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

        return RightJoinIterator(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
    }

    private static IEnumerable<TResult> RightJoinIterator<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter?, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
    {
        using var innerEnumerator = inner.GetEnumerator();
        if (!innerEnumerator.MoveNext())
        {
            yield break;
        }

        var outerLookup = outer.ToLookup(outerKeySelector, comparer);
        do
        {
            var innerItem = innerEnumerator.Current;
            var outerGroup = outerLookup[innerKeySelector(innerItem)];
            using var outerGroupEnumerator = outerGroup.GetEnumerator();
            if (outerGroupEnumerator.MoveNext())
            {
                do
                {
                    yield return resultSelector(outerGroupEnumerator.Current, innerItem);
                }
                while (outerGroupEnumerator.MoveNext());
            }
            else
            {
                yield return resultSelector(default, innerItem);
            }
        }
        while (innerEnumerator.MoveNext());
    }
}
