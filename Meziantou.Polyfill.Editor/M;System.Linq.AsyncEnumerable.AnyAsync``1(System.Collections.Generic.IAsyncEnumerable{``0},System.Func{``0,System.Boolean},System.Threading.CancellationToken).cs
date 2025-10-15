using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<bool> AnyAsync<TSource>(
        this IAsyncEnumerable<TSource> source,
        Func<TSource, bool> predicate,
        CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, cancellationToken);

        static async ValueTask<bool> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken)
        {
            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                if (predicate(element))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
