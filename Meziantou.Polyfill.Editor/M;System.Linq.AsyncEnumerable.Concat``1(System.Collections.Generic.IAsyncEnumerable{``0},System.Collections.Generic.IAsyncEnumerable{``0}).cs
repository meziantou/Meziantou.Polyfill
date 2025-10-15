using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> Concat<TSource>(
            this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second)
    {
        if (first is null)
            throw new ArgumentNullException(nameof(first));

        if (second is null)
            throw new ArgumentNullException(nameof(second));

        return Impl(first, second, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> first,
            IAsyncEnumerable<TSource> second,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (TSource item in first.WithCancellation(cancellationToken))
            {
                yield return item;
            }

            await foreach (TSource item in second.WithCancellation(cancellationToken))
            {
                yield return item;
            }
        }
    }
}
