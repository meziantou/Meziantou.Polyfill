using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> ExceptBy<TSource, TKey>(
       this IAsyncEnumerable<TSource> first,
       IAsyncEnumerable<TKey> second,
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
            IAsyncEnumerable<TKey> second,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> firstEnumerator = first.GetAsyncEnumerator(cancellationToken);

            if (!await firstEnumerator.MoveNextAsync())
            {
                yield break;
            }

            HashSet<TKey> set = new(comparer);

            await foreach (TKey key in second.WithCancellation(cancellationToken))
            {
                set.Add(key);
            }

            do
            {
                TSource firstElement = firstEnumerator.Current;
                if (set.Add(keySelector(firstElement)))
                {
                    yield return firstElement;
                }
            }
            while (await firstEnumerator.MoveNextAsync());
        }
    }
}
