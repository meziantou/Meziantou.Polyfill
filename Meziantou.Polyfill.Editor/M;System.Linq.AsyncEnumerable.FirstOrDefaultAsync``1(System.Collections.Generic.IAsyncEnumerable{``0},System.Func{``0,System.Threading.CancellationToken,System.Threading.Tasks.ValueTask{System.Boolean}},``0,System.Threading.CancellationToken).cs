using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource> FirstOrDefaultAsync<TSource>(
           this IAsyncEnumerable<TSource> source,
           Func<TSource, CancellationToken, ValueTask<bool>> predicate,
           TSource defaultValue,
           CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, defaultValue, cancellationToken);

        static async ValueTask<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, ValueTask<bool>> predicate,
            TSource defaultValue,
            CancellationToken cancellationToken)
        {
            await foreach (TSource item in source.WithCancellation(cancellationToken))
            {
                if (await predicate(item, cancellationToken))
                {
                    return item;
                }
            }

            return defaultValue;
        }
    }
}
