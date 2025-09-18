using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<long> LongCountAsync<TSource>(
              this IAsyncEnumerable<TSource> source,
              CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source, cancellationToken);

        static async ValueTask<long> Impl(
            IAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            long count = 0;
            while (await e.MoveNextAsync())
            {
                count++;
            }

            return count;
        }
    }
}
