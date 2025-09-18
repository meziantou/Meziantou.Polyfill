using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> DistinctBy<TSource, TKey>(
          this IAsyncEnumerable<TSource> source,
          Func<TSource, CancellationToken, ValueTask<TKey>> keySelector,
          IEqualityComparer<TKey>? comparer = null)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (keySelector is null)
            throw new ArgumentNullException(nameof(keySelector));

        return Impl(source, keySelector, comparer, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, ValueTask<TKey>> keySelector,
            IEqualityComparer<TKey>? comparer,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            if (await e.MoveNextAsync())
            {
                HashSet<TKey> set = new(comparer);
                do
                {
                    TSource element = e.Current;
                    if (set.Add(await keySelector(element, cancellationToken)))
                    {
                        yield return element;
                    }
                }
                while (await e.MoveNextAsync());
            }
        }
    }
}
