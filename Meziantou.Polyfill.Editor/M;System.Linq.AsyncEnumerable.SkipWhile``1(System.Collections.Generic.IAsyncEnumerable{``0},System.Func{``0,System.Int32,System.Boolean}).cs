using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> SkipWhile<TSource>(
         this IAsyncEnumerable<TSource> source,
         Func<TSource, int, bool> predicate)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return Impl(source, predicate, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, int, bool> predicate,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            int index = -1;
            while (await e.MoveNextAsync())
            {
                TSource element = e.Current;
                if (!predicate(element, checked(++index)))
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
