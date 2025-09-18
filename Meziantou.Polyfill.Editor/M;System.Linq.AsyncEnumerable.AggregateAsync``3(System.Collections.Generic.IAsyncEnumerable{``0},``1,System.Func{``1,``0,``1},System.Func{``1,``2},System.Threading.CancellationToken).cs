using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TResult> AggregateAsync<TSource, TAccumulate, TResult>(
         this IAsyncEnumerable<TSource> source,
         TAccumulate seed,
         Func<TAccumulate, TSource, TAccumulate> func,
         Func<TAccumulate, TResult> resultSelector,
         CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (func == null)
            throw new ArgumentNullException(nameof(func));
        if (resultSelector == null)
            throw new ArgumentNullException(nameof(resultSelector));

        return Impl(source.WithCancellation(cancellationToken), seed, func, resultSelector);

        static async ValueTask<TResult> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func,
            Func<TAccumulate, TResult> resultSelector)
        {
            TAccumulate result = seed;

            await foreach (TSource element in source)
            {
                result = func(result, element);
            }

            return resultSelector(result);
        }
    }
}
