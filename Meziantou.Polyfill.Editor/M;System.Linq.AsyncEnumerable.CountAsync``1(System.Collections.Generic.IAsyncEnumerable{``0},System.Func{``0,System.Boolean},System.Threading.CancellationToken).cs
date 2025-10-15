using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<int> CountAsync<TSource>(
           this IAsyncEnumerable<TSource> source,
           Func<TSource, bool> predicate,
           CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source.WithCancellation(cancellationToken), predicate);

        static async ValueTask<int> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            int count = 0;
            await foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    checked { count++; }
                }
            }

            return count;
        }
    }
}
