using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource> FirstOrDefaultAsync<TSource>(
         this IAsyncEnumerable<TSource> source,
         TSource defaultValue,
         CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source, defaultValue, cancellationToken);

        static async ValueTask<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            TSource defaultValue,
            CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            return await e.MoveNextAsync() ? e.Current : defaultValue;
        }
    }
}
