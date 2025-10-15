using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> Take<TSource>(
       this IAsyncEnumerable<TSource> source,
       int count)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source, count, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            int count,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (TSource element in source.WithCancellation(cancellationToken))
            {
                yield return element;

                if (--count == 0)
                {
                    break;
                }
            }
        }
    }
}
