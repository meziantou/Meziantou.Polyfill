using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<HashSet<TSource>> ToHashSetAsync<TSource>(
            this IAsyncEnumerable<TSource> source,
            IEqualityComparer<TSource>? comparer = null,
            CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken), comparer);

        static async ValueTask<HashSet<TSource>> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            IEqualityComparer<TSource>? comparer)
        {
            HashSet<TSource> set = new(comparer);
            await foreach (TSource element in source)
            {
                set.Add(element);
            }

            return set;
        }
    }
}
