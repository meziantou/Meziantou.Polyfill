using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource> LastAsync<TSource>(
      this IAsyncEnumerable<TSource> source,
      Func<TSource, CancellationToken, ValueTask<bool>> predicate,
      CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, cancellationToken);

        static async ValueTask<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, ValueTask<bool>> predicate,
            CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            while (await e.MoveNextAsync())
            {
                TSource element = e.Current;
                if (await predicate(element, cancellationToken))
                {
                    TSource result = element;

                    while (await e.MoveNextAsync())
                    {
                        element = e.Current;
                        if (await predicate(element, cancellationToken))
                        {
                            result = element;
                        }
                    }

                    return result;
                }
            }

            throw new InvalidOperationException("Sequence contains no matching element");
        }
    }
}
