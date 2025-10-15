using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<int> CountAsync<TSource>(
          this IAsyncEnumerable<TSource> source,
          CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source, cancellationToken);

        static async ValueTask<int> Impl(
            IAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            int count = 0;
            while (await e.MoveNextAsync())
            {
                checked { count++; }
            }

            return count;
        }
    }
}
