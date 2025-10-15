using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<bool> AnyAsync<TSource>(
          this IAsyncEnumerable<TSource> source,
          CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source, cancellationToken);

        static async ValueTask<bool> Impl(
            IAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            return await e.MoveNextAsync();
        }
    }
}
