using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(
        this IAsyncEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        IEqualityComparer<TKey>? comparer = null,
        CancellationToken cancellationToken = default) where TKey : notnull
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (keySelector is null)
            throw new ArgumentNullException(nameof(keySelector));

        return Impl(source.WithCancellation(cancellationToken), keySelector, comparer);

        static async ValueTask<Dictionary<TKey, TSource>> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer)
        {
            Dictionary<TKey, TSource> d = new(comparer);
            await foreach (TSource element in source)
            {
                d.Add(keySelector(element), element);
            }
            return d;
        }
    }
}
