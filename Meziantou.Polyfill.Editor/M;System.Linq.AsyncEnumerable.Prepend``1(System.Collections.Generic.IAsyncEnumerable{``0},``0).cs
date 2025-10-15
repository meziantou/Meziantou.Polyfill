using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> Prepend<TSource>(
           this IAsyncEnumerable<TSource> source,
           TSource element)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source, element, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            TSource element,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            yield return element;

            await foreach (TSource item in source.WithCancellation(cancellationToken))
            {
                yield return item;
            }
        }
    }
}
