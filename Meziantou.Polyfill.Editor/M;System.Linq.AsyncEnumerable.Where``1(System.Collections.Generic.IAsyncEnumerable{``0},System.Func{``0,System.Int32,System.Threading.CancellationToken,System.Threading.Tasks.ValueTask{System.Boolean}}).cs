using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> Where<TSource>(
              this IAsyncEnumerable<TSource> source,
              Func<TSource, int, CancellationToken, ValueTask<bool>> predicate)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, ValueTask<bool>> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            int index = -1;
            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                if (await predicate(element, checked(++index), cancellationToken))
                {
                    yield return element;
                }
            }
        }
    }
}
