// XML-DOC: M:System.Linq.AsyncEnumerable.AggregateBy``3(System.Collections.Generic.IAsyncEnumerable{``0},System.Func{``0,System.Threading.CancellationToken,System.Threading.Tasks.ValueTask{``1}},System.Func{``1,System.Threading.CancellationToken,System.Threading.Tasks.ValueTask{``2}},System.Func{``2,``0,System.Threading.CancellationToken,System.Threading.Tasks.ValueTask{``2}},System.Collections.Generic.IEqualityComparer{``1})
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static IAsyncEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(
         this IAsyncEnumerable<TSource> source,
         Func<TSource, CancellationToken, ValueTask<TKey>> keySelector,
         Func<TKey, CancellationToken, ValueTask<TAccumulate>> seedSelector,
         Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> func,
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
            Func<TSource, CancellationToken, ValueTask<TKey>> keySelector,
            Func<TKey, CancellationToken, ValueTask<TAccumulate>> seedSelector,
            Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> func,
            IEqualityComparer<TKey>? keyComparer,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            if (await e.MoveNextAsync())
            {
                Dictionary<TKey, TAccumulate> dict = new(keyComparer);

                do
                {
                    TSource value = e.Current;
                    TKey key = await keySelector(value, cancellationToken);

                    dict[key] = await func(
                        dict.TryGetValue(key, out TAccumulate? acc) ? acc : await seedSelector(key, cancellationToken),
                        value,
                        cancellationToken);
                }
                while (await e.MoveNextAsync());

                foreach (KeyValuePair<TKey, TAccumulate> countBy in dict)
                {
                    yield return countBy;
                }
            }
        }
    }
}
