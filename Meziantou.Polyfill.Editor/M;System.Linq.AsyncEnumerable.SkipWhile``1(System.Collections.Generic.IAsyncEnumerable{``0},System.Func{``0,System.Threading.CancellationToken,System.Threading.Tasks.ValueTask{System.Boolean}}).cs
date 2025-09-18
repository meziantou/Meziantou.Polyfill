using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> SkipWhile<TSource>(
          this IAsyncEnumerable<TSource> source,
          Func<TSource, CancellationToken, ValueTask<bool>> predicate)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, ValueTask<bool>> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            while (await e.MoveNextAsync())
            {
                TSource element = e.Current;
                if (!await predicate(element, cancellationToken))
                {
                    yield return element;
                    while (await e.MoveNextAsync())
                    {
                        yield return e.Current;
                    }

                    yield break;
                }
            }
        }
    }
}
