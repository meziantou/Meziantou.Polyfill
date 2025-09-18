using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(
        this IAsyncEnumerable<TSource> source,
        TAccumulate seed,
        Func<TAccumulate, TSource, TAccumulate> func,
        CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (func == null)
            throw new ArgumentNullException(nameof(func));

        return Impl(source.WithCancellation(cancellationToken), seed, func);

        static async ValueTask<TAccumulate> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            TAccumulate result = seed;

            await foreach (TSource element in source)
            {
                result = func(result, element);
            }

            return result;
        }
    }
}
