using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<long> LongCountAsync<TSource>(
           this IAsyncEnumerable<TSource> source,
           Func<TSource, bool> predicate,
           CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source.WithCancellation(cancellationToken), predicate);

        static async ValueTask<long> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            long count = 0;
            await foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
