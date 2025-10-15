using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource> FirstOrDefaultAsync<TSource>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            TSource defaultValue,
            CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source.WithCancellation(cancellationToken), predicate, defaultValue);

        static async ValueTask<TSource> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            TSource defaultValue)
        {
            await foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return defaultValue;
        }
    }
}
