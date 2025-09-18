using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(
           this IAsyncEnumerable<TSource> source,
           Func<TSource, IEnumerable<TResult>> selector)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        return Impl(source, selector, default);

        async static IAsyncEnumerable<TResult> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                foreach (TResult subElement in selector(element))
                {
                    yield return subElement;
                }
            }
        }
    }
}
