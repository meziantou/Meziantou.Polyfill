using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<long> LongCountAsync<TSource>(
           this IAsyncEnumerable<TSource> source,
           Func<TSource, CancellationToken, ValueTask<bool>> predicate,
           CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, cancellationToken);

        static async ValueTask<long> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, ValueTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            long count = 0;
            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                if (await predicate(element, cancellationToken))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
