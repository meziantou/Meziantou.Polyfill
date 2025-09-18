using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource> SingleOrDefaultAsync<TSource>(
          this IAsyncEnumerable<TSource> source,
          Func<TSource, CancellationToken, ValueTask<bool>> predicate,
          TSource defaultValue,
          CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, defaultValue, cancellationToken);

        static async ValueTask<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, ValueTask<bool>> predicate,
            TSource defaultValue,
            CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            while (await e.MoveNextAsync())
            {
                TSource result = e.Current;
                if (await predicate(result, cancellationToken))
                {
                    while (await e.MoveNextAsync())
                    {
                        if (await predicate(e.Current, cancellationToken))
                        {
                            throw new InvalidOperationException("Sequence contains more than one matching element");
                        }
                    }

                    return result;
                }
            }

            return defaultValue;
        }
    }
}
