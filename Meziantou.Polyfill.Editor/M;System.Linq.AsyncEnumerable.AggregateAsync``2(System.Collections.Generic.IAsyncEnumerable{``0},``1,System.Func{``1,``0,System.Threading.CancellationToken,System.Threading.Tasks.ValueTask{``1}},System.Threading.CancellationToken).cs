using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(
       this IAsyncEnumerable<TSource> source, TAccumulate seed,
       Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> func,
       CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (func == null)
            throw new ArgumentNullException(nameof(func));

        return Impl(source, seed, func, cancellationToken);

        static async ValueTask<TAccumulate> Impl(
            IAsyncEnumerable<TSource> source, TAccumulate seed,
            Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> func,
            CancellationToken cancellationToken = default)
        {
            TAccumulate result = seed;

            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                result = await func(result, element, cancellationToken);
            }

            return result;
        }
    }
}
