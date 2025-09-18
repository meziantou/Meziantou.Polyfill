using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> Union<TSource>(
           this IAsyncEnumerable<TSource> first,
           IAsyncEnumerable<TSource> second,
           IEqualityComparer<TSource>? comparer = null)
    {
        if (first is null)
            throw new ArgumentNullException(nameof(first));
        if (second is null)
            throw new ArgumentNullException(nameof(second));

        return Impl(first, second, comparer, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> first,
            IAsyncEnumerable<TSource> second,
            IEqualityComparer<TSource>? comparer,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            HashSet<TSource> set = new(comparer);

            await foreach (TSource element in first.WithCancellation(cancellationToken))
            {
                if (set.Add(element))
                {
                    yield return element;
                }
            }

            await foreach (TSource element in second.WithCancellation(cancellationToken))
            {
                if (set.Add(element))
                {
                    yield return element;
                }
            }
        }
    }
}
