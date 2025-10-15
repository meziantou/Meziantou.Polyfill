using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(
          this IAsyncEnumerable<TSource> source,
          Func<TSource, TKey> keySelector,
          Func<TKey, TAccumulate> seedSelector,
          Func<TAccumulate, TSource, TAccumulate> func,
          IEqualityComparer<TKey>? keyComparer = null) where TKey : notnull
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (keySelector == null)
            throw new ArgumentNullException(nameof(keySelector));
        if (seedSelector == null)
            throw new ArgumentNullException(nameof(seedSelector));
        if (func == null)
            throw new ArgumentNullException(nameof(func));

        return Impl(source, keySelector, seedSelector, func, keyComparer, default);

        static async IAsyncEnumerable<KeyValuePair<TKey, TAccumulate>> Impl(
            IAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TKey, TAccumulate> seedSelector,
            Func<TAccumulate, TSource, TAccumulate> func,
            IEqualityComparer<TKey>? keyComparer,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            if (!await e.MoveNextAsync())
            {
                yield break;
            }

            Dictionary<TKey, TAccumulate> dict = new(keyComparer);

            do
            {
                TSource value = e.Current;
                TKey key = keySelector(value);

#if NET9_0_OR_GREATER
                ref TAccumulate? acc = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(dict, key, out bool exists);
                acc = func(exists ? acc! : seedSelector(key), value);
#else
                dict[key] = func(dict.TryGetValue(key, out TAccumulate? acc) ? acc : seedSelector(key), value);
#endif
            }
            while (await e.MoveNextAsync());

            foreach (KeyValuePair<TKey, TAccumulate> countBy in dict)
            {
                yield return countBy;
            }
        }
    }
}
