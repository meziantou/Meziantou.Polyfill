using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> SkipWhile<TSource>(
         this IAsyncEnumerable<TSource> source,
         Func<TSource, bool> predicate)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            while (await e.MoveNextAsync())
            {
                TSource element = e.Current;
                if (!predicate(element))
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
