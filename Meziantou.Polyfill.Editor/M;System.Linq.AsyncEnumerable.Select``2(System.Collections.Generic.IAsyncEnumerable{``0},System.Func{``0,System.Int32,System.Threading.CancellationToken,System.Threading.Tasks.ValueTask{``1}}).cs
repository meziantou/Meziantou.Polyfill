using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TResult> Select<TSource, TResult>(
         this IAsyncEnumerable<TSource> source,
         Func<TSource, int, CancellationToken, ValueTask<TResult>> selector)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        return Impl(source, selector, default);

        static async IAsyncEnumerable<TResult> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, ValueTask<TResult>> selector,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            int index = -1;
            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                yield return await selector(element, checked(++index), cancellationToken);
            }
        }
    }
}
