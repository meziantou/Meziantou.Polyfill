using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<TSource> Distinct<TSource>(
    this IAsyncEnumerable<TSource> source,
    IEqualityComparer<TSource>? comparer = null)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source, comparer, default);

        static async IAsyncEnumerable<TSource> Impl(
            IAsyncEnumerable<TSource> source,
            IEqualityComparer<TSource>? comparer,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            if (await e.MoveNextAsync())
            {
                HashSet<TSource> set = new(comparer);
                do
                {
                    TSource element = e.Current;
                    if (set.Add(element))
                    {
                        yield return element;
                    }
                }
                while (await e.MoveNextAsync());
            }
        }
    }
}
