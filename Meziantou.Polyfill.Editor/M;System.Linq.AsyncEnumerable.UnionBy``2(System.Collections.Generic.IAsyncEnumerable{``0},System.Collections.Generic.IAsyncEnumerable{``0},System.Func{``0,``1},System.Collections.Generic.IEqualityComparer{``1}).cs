using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> UnionBy<TSource, TKey>(
           this IAsyncEnumerable<TSource> first,
           IAsyncEnumerable<TSource> second,
           Func<TSource, TKey> keySelector,
           IEqualityComparer<TKey>? comparer = null)
    {
        if (first is null)
            throw new ArgumentNullException(nameof(first));
        if (second is null)
            throw new ArgumentNullException(nameof(second));
        if (keySelector is null)
            throw new ArgumentNullException(nameof(keySelector));

        return Impl(first, second, keySelector, comparer, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> first,
            IAsyncEnumerable<TSource> second,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            HashSet<TKey> set = new(comparer);

            await foreach (TSource element in first.WithCancellation(cancellationToken))
            {
                if (set.Add(keySelector(element)))
                {
                    yield return element;
                }
            }

            await foreach (TSource element in second.WithCancellation(cancellationToken))
            {
                if (set.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
