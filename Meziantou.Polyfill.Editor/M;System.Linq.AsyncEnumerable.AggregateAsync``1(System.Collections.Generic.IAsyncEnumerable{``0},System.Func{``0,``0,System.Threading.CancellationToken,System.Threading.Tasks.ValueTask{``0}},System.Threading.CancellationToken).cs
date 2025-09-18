using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource> AggregateAsync<TSource>(
          this IAsyncEnumerable<TSource> source,
          Func<TSource, TSource, CancellationToken, ValueTask<TSource>> func,
          CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (func == null)
            throw new ArgumentNullException(nameof(func));

        return Impl(source, func, cancellationToken);

        static async ValueTask<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, TSource, CancellationToken, ValueTask<TSource>> func,
            CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            if (!await e.MoveNextAsync())
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            TSource result = e.Current;
            while (await e.MoveNextAsync())
            {
                result = await func(result, e.Current, cancellationToken);
            }

            return result;
        }
    }
}
