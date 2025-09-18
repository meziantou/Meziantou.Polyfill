using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(
        this IAsyncEnumerable<KeyValuePair<TKey, TValue>> source,
        IEqualityComparer<TKey>? comparer = null,
        CancellationToken cancellationToken = default) where TKey : notnull
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return Impl(source.WithCancellation(cancellationToken), comparer);

        static async ValueTask<Dictionary<TKey, TValue>> Impl(
            ConfiguredCancelableAsyncEnumerable<KeyValuePair<TKey, TValue>> source,
            IEqualityComparer<TKey>? comparer)
        {
            Dictionary<TKey, TValue> d = new Dictionary<TKey, TValue>(comparer);
            await foreach (KeyValuePair<TKey, TValue> element in source)
            {
                d.Add(element.Key, element.Value);
            }

            return d;
        }
    }
}
