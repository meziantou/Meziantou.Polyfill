using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource> LastAsync<TSource>(
          this IAsyncEnumerable<TSource> source,
          Func<TSource, bool> predicate,
          CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, cancellationToken);

        static async ValueTask<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            while (await e.MoveNextAsync())
            {
                TSource element = e.Current;
                if (predicate(element))
                {
                    TSource result = element;

                    while (await e.MoveNextAsync())
                    {
                        element = e.Current;
                        if (predicate(element))
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
