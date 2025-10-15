using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> Skip<TSource>(
           this IAsyncEnumerable<TSource> source,
           int count)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return count <= 0 ? source : Impl(source, count, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            int count,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            while (count > 0 && await e.MoveNextAsync())
            {
                count--;
            }

            if (count <= 0)
            {
                while (await e.MoveNextAsync())
                {
                    yield return e.Current;
                }
            }
        }
    }
}
