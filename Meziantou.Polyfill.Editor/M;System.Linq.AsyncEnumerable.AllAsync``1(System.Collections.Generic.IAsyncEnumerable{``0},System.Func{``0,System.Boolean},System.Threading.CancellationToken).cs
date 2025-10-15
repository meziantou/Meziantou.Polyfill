using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<bool> AllAsync<TSource>(
          this IAsyncEnumerable<TSource> source,
          Func<TSource, bool> predicate,
          CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source.WithCancellation(cancellationToken), predicate);

        static async ValueTask<bool> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            await foreach (TSource element in source)
            {
                if (!predicate(element))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
