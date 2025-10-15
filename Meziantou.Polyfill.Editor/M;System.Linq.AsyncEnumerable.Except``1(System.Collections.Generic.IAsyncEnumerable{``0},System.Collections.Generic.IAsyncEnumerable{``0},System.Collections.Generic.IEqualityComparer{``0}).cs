using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> Except<TSource>(
           this IAsyncEnumerable<TSource> first,
           IAsyncEnumerable<TSource> second,
           IEqualityComparer<TSource>? comparer = null)
    {
        if (first is null)
            throw new ArgumentNullException(nameof(first));
        if (second is null)
            throw new ArgumentNullException(nameof(second));

        return Impl(first, second, comparer, default);

        async static IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> first,
            IAsyncEnumerable<TSource> second,
            IEqualityComparer<TSource>? comparer,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> firstEnumerator = first.GetAsyncEnumerator(cancellationToken);

            if (!await firstEnumerator.MoveNextAsync())
            {
                yield break;
            }

            HashSet<TSource> set = new(comparer);

            await foreach (TSource element in second.WithCancellation(cancellationToken))
            {
                set.Add(element);
            }

            do
            {
                TSource firstElement = firstEnumerator.Current;
                if (set.Add(firstElement))
                {
                    yield return firstElement;
                }
            }
            while (await firstEnumerator.MoveNextAsync());
        }
    }
}
