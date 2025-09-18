using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource> LastAsync<TSource>(
         this IAsyncEnumerable<TSource> source,
         CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source, cancellationToken);

        static async ValueTask<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            if (!await e.MoveNextAsync())
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            TSource result;
            do
            {
                result = e.Current;
            }
            while (await e.MoveNextAsync());

            return result;
        }
    }
}
