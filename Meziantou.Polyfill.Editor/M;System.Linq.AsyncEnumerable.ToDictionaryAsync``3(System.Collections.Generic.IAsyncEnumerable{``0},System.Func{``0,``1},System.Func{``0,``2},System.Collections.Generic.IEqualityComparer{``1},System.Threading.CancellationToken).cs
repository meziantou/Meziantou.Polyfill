using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(
         this IAsyncEnumerable<TSource> source,
         Func<TSource, TKey> keySelector,
         Func<TSource, TElement> elementSelector,
         IEqualityComparer<TKey>? comparer = null,
         CancellationToken cancellationToken = default) where TKey : notnull
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (keySelector is null)
            throw new ArgumentNullException(nameof(keySelector));
        if (elementSelector is null)
            throw new ArgumentNullException(nameof(elementSelector));

        return Impl(source.WithCancellation(cancellationToken), keySelector, elementSelector, comparer);

        static async ValueTask<Dictionary<TKey, TElement>> Impl(
            ConfiguredCancelableAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey>? comparer)
        {
            Dictionary<TKey, TElement> d = new(comparer);
            await foreach (TSource element in source)
            {
                d.Add(keySelector(element), elementSelector(element));
            }

            return d;
        }
    }
}
